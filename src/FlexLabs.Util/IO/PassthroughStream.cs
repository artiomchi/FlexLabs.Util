using System;
using System.Collections.Concurrent;
using System.IO;

namespace FlexLabs.IO
{
    public class PassthroughStream : Stream
    {
        private readonly long _length;
        private readonly BlockingCollection<byte[]> _buffer;
        private byte[] _overflow = null;
        private long _read = 0;
        public PassthroughStream(long length)
        {
            _length = length;
            _buffer = new BlockingCollection<byte[]>();
        }

        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => _length;
        public override long Position { get => _read; set => throw new NotImplementedException(); }
        public override void Flush() { }

        public override long Seek(long offset, SeekOrigin origin) => throw new InvalidOperationException();
        public override void SetLength(long value) => throw new InvalidOperationException();

        public override int Read(byte[] buffer, int offset, int count)
        {
            int read = 0;
            byte[] local = null;
            if (_overflow != null)
            {
                local = _overflow;
                _overflow = null;
            }

            int localRead;
            do
            {
                if (local == null)
                {
                    try
                    {
                        local = _buffer.Take();
                    }
                    catch (InvalidOperationException)
                    {
                        _read += read;
                        return read;
                    }
                }

                localRead = Math.Min(count - read, local.Length);
                Array.Copy(local, 0, buffer, read + offset, localRead);
                read += localRead;

                if (localRead == local.Length)
                    local = null;
            }
            while (read < count);

            if (local != null && localRead < local.Length)
            {
                _overflow = new byte[local.Length - localRead];
                Array.Copy(local, localRead, _overflow, 0, _overflow.Length);
            }

            _read += read;
            return read;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            var local = new byte[count];
            Array.Copy(buffer, offset, local, 0, count);
            _buffer.Add(local);
        }

        public void Complete() => _buffer.CompleteAdding();
    }
}
