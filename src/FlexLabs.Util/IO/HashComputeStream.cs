using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace FlexLabs.IO
{
    /// <summary>
    /// Stream wrapper that will calculate the checksum of the data passed through it on the fly
    /// </summary>
    public class HashComputeStream : Stream
    {
        private readonly Stream _source;
        private readonly HashAlgorithm _hashAlgorithm;
        private readonly PassthroughStream _passthroughStream;
        private readonly Task<byte[]> _hashTask;
        private bool? _readingStream = null;

        /// <summary>
        /// </summary>
        /// <param name="source">Source/destination stream</param>
        /// <param name="hashAlgorithm">Hash algorythm to use</param>
        public HashComputeStream(Stream source, HashAlgorithm hashAlgorithm)
        {
            _source = source;
            _hashAlgorithm = hashAlgorithm;
            _passthroughStream = new PassthroughStream(_source.Length);
            _hashTask = Task.Run((Func<byte[]>)CalculateHash);
        }

        /// <summary>
        /// </summary>
        /// <param name="source">Source/destination stream</param>
        /// <param name="hashAlgorithm">Hash algorythm to use</param>
        /// <param name="readingStream">Boolean stating whether the stream will be used for reading or writing</param>
        public HashComputeStream(Stream source, HashAlgorithm hashAlgorithm, bool readingStream)
            : this(source, hashAlgorithm)
        {
            _readingStream = readingStream;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _source.Dispose();
            _passthroughStream.Complete();
            _passthroughStream.Dispose();
        }

        private byte[] CalculateHash()
        {
            var hash = _hashAlgorithm.ComputeHash(_passthroughStream);
            return hash;
        }

        public override bool CanRead => _readingStream == true;
        public override bool CanSeek => false;
        public override bool CanWrite => _readingStream == false;
        public override long Length => _source.Length;
        public override long Position { get => _source.Position; set => throw new InvalidOperationException(); }
        public override void Flush() => _source.Flush();

        public override long Seek(long offset, SeekOrigin origin) => throw new InvalidOperationException();
        public override void SetLength(long value) => throw new InvalidOperationException();

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_readingStream == null)
                _readingStream = true;
            if (_readingStream != true)
                throw new InvalidOperationException();

            var read = _source.Read(buffer, offset, count);
            if (read > 0)
            {
                _passthroughStream.Write(buffer, offset, read);
            }
            return read;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_readingStream == null)
                _readingStream = false;
            if (_readingStream != false)
                throw new InvalidOperationException();

            _source.Write(buffer, offset, count);
            _passthroughStream.Write(buffer, offset, count);
        }

        /// <summary>
        /// Once finished writing to the stream, call this method to stop the hash algorythm and get the resulting checksum
        /// </summary>
        /// <returns>The checksum of the data passed through the stream</returns>
        public byte[] CompleteAndGetHash()
        {
            _passthroughStream.Complete();
            return _hashTask.GetAwaiter().GetResult();
        }
    }
}
