using System;
using System.Globalization;
using Xunit;

namespace FlexLabs.Util.Tests
{
    public class TypeConvertTests
    {
        [Fact]
        public void TypeConvert_Int()
        {
            var value = TypeConvert.ToType("123", typeof(int));
            Assert.IsType<int>(value);
            Assert.Equal(123, value);
        }

        [Fact]
        public void TypeConvert_Impl_Int()
        {
            var value = TypeConvert.To<int>("123");
            Assert.IsType<int>(value);
            Assert.Equal(123, value);
        }

        [Fact]
        public void TypeConvert_IntNull()
        {
            var value = TypeConvert.ToType("123", typeof(int?));
            Assert.IsType<int>(value);
            Assert.Equal(123, value);
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
            Assert.IsType<int>(value);
            Assert.Equal(123, value);
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
            Assert.IsType<StringComparison>(value);
            Assert.Equal(StringComparison.OrdinalIgnoreCase, value);
        }

        [Fact]
        public void TypeConvert_Impl_Enum()
        {
            var value = TypeConvert.To<StringComparison>("OrdinalIgnoreCase");
            Assert.IsType<StringComparison>(value);
            Assert.Equal(StringComparison.OrdinalIgnoreCase, value);
        }

        [Fact]
        public void TypeConvert_EnumNull()
        {
            var value = TypeConvert.ToType("OrdinalIgnoreCase", typeof(StringComparison?));
            Assert.IsType<StringComparison>(value);
            Assert.Equal(StringComparison.OrdinalIgnoreCase, value);
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
            Assert.IsType<StringComparison>(value);
            Assert.Equal(StringComparison.OrdinalIgnoreCase, value);
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
            Assert.IsType<bool>(value);
            Assert.True(value);
        }

        [Fact]
        public void TypeConvert_Guid()
        {
            var guid = Guid.NewGuid();
            var value = TypeConvert.To<Guid>(guid.ToString());
            Assert.IsType<Guid>(value);
            Assert.Equal(value, guid);
        }

        [Fact]
        public void TypeConvert_GuidNull()
        {
            var guid = Guid.NewGuid();
            var value = TypeConvert.To<Guid?>(guid.ToString());
            Assert.IsType<Guid>(value);
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
            var date = DateTime.Parse(dateStr, CultureInfo.InvariantCulture);
            var value = TypeConvert.To<DateTime>(dateStr);
            Assert.IsType<DateTime>(value);
            Assert.Equal(date, value);
        }

        [Fact]
        public void TypeConvert_DateTimeNull()
        {
            var dateStr = DateTime.Now.ToString();
            var date = DateTime.Parse(dateStr);
            var value = TypeConvert.To<DateTime?>(dateStr);
            Assert.IsType<DateTime>(value);
            Assert.Equal(value, date);
        }

        [Fact]
        public void TypeConvert_DateTimeActualNull()
        {
            var value = TypeConvert.To<DateTime?>("");
            Assert.Null(value);
        }

        [Theory]
        [InlineData("en-US")]
        [InlineData("es-ES")]
        public void TypeConvert_Decimal(string culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
            Assert.Equal(0.2m, TypeConvert.To<decimal>("0.2"));
            Assert.Equal(2, TypeConvert.To<decimal>("0,2"));
        }

        [Theory]
        [InlineData("en-US")]
        [InlineData("es-ES")]
        public void TypeConvert_Double(string culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
            Assert.Equal(0.2, TypeConvert.To<double>("0.2"));
            Assert.Equal(2, TypeConvert.To<double>("0,2"));
        }
    }
}
