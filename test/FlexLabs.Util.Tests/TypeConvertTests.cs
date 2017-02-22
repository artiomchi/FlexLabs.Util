using System;
using Xunit;

namespace FlexLabs.Util.Tests
{
    public class TypeConvertTests
    {
        [Fact]
        public void TypeConvert_Int()
        {
            var value = TypeConvert.ToType("123", typeof(int));
            Assert.IsType(typeof(int), value);
            Assert.Equal(value, 123);
        }

        [Fact]
        public void TypeConvert_Impl_Int()
        {
            var value = TypeConvert.To<int>("123");
            Assert.IsType(typeof(int), value);
            Assert.Equal(value, 123);
        }

        [Fact]
        public void TypeConvert_IntNull()
        {
            var value = TypeConvert.ToType("123", typeof(int?));
            Assert.IsType(typeof(int), value);
            Assert.Equal(value, 123);
        }

        [Fact]
        public void TypeConvert_IntActualNull()
        {
            var value = TypeConvert.ToType("", typeof(int?));
            Assert.Null(value);
        }

        [Fact]
        public void TypeConvert_Impl_IntNull()
        {
            var value = TypeConvert.To<int?>("123");
            Assert.IsType(typeof(int), value);
            Assert.Equal(value, 123);
        }

        [Fact]
        public void TypeConvert_Impl_IntActualNull()
        {
            var value = TypeConvert.To<int?>("");
            Assert.Null(value);
        }

        [Fact]
        public void TypeConvert_Enum()
        {
            var value = TypeConvert.ToType("OrdinalIgnoreCase", typeof(StringComparison));
            Assert.IsType(typeof(StringComparison), value);
            Assert.Equal(value, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void TypeConvert_Impl_Enum()
        {
            var value = TypeConvert.To<StringComparison>("OrdinalIgnoreCase");
            Assert.IsType(typeof(StringComparison), value);
            Assert.Equal(value, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void TypeConvert_EnumNull()
        {
            var value = TypeConvert.ToType("OrdinalIgnoreCase", typeof(StringComparison?));
            Assert.IsType(typeof(StringComparison), value);
            Assert.Equal(value, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void TypeConvert_IntEnumActualNull()
        {
            var value = TypeConvert.ToType("", typeof(StringComparison?));
            Assert.Null(value);
        }

        [Fact]
        public void TypeConvert_Impl_EnumNull()
        {
            var value = TypeConvert.To<StringComparison?>("OrdinalIgnoreCase");
            Assert.IsType(typeof(StringComparison), value);
            Assert.Equal(value, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void TypeConvert_Impl_EnumActualNull()
        {
            var value = TypeConvert.To<StringComparison?>("");
            Assert.Null(value);
        }

        [Fact]
        public void TypeConvert_Bool()
        {
            var value = TypeConvert.To<bool>("true");
            Assert.IsType(typeof(bool), value);
            Assert.Equal(value, true);
        }

        [Fact]
        public void TypeConvert_Guid()
        {
            var guid = Guid.NewGuid();
            var value = TypeConvert.To<Guid>(guid.ToString());
            Assert.IsType(typeof(Guid), value);
            Assert.Equal(value, guid);
        }

        [Fact]
        public void TypeConvert_GuidNull()
        {
            var guid = Guid.NewGuid();
            var value = TypeConvert.To<Guid?>(guid.ToString());
            Assert.IsType(typeof(Guid), value);
            Assert.Equal(value, guid);
        }

        [Fact]
        public void TypeConvert_GuidActualNull()
        {
            var value = TypeConvert.To<Guid?>("");
            Assert.Null(value);
        }

        [Theory]
        [InlineData("2012-05-18")]
        [InlineData("2012-05-18 12:30")]
        [InlineData("1/2/2012 12:30")]
        [InlineData("1/2/2012 12:30:12")]
        public void TypeConvert_DateTime(string dateStr)
        {
            var date = DateTime.Parse(dateStr);
            var value = TypeConvert.To<DateTime>(dateStr);
            Assert.IsType(typeof(DateTime), value);
            Assert.Equal(value, date);
        }

        [Fact]
        public void TypeConvert_DateTimeNull()
        {
            var dateStr = DateTime.Now.ToString();
            var date = DateTime.Parse(dateStr);
            var value = TypeConvert.To<DateTime?>(dateStr);
            Assert.IsType(typeof(DateTime), value);
            Assert.Equal(value, date);
        }

        [Fact]
        public void TypeConvert_DateTimeActualNull()
        {
            var value = TypeConvert.To<DateTime?>("");
            Assert.Null(value);
        }
    }
}
