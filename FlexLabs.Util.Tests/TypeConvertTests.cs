using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexLabs.Util.Tests
{
    [TestClass]
    public class TypeConvertTests
    {
        [TestMethod]
        public void TypeConvert_Int()
        {
            var value = TypeConvert.ToType("123", typeof(Int32));
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(value, 123);
        }

        [TestMethod]
        public void TypeConvert_Impl_Int()
        {
            var value = TypeConvert.To<Int32>("123");
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(value, 123);
        }

        [TestMethod]
        public void TypeConvert_IntNull()
        {
            var value = TypeConvert.ToType("123", typeof(Int32?));
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(value, 123);
        }

        [TestMethod]
        public void TypeConvert_IntActualNull()
        {
            var value = TypeConvert.ToType("", typeof(Int32?));
            Assert.IsNull(value);
        }

        [TestMethod]
        public void TypeConvert_Impl_IntNull()
        {
            var value = TypeConvert.To<Int32?>("123");
            Assert.IsInstanceOfType(value, typeof(int));
            Assert.AreEqual(value, 123);
        }

        [TestMethod]
        public void TypeConvert_Impl_IntActualNull()
        {
            var value = TypeConvert.To<Int32?>("");
            Assert.IsNull(value);
        }

        [TestMethod]
        public void TypeConvert_Enum()
        {
            var value = TypeConvert.ToType("OrdinalIgnoreCase", typeof(StringComparison));
            Assert.IsInstanceOfType(value, typeof(StringComparison));
            Assert.AreEqual(value, StringComparison.OrdinalIgnoreCase);
        }

        [TestMethod]
        public void TypeConvert_Impl_Enum()
        {
            var value = TypeConvert.To<StringComparison>("OrdinalIgnoreCase");
            Assert.IsInstanceOfType(value, typeof(StringComparison));
            Assert.AreEqual(value, StringComparison.OrdinalIgnoreCase);
        }

        [TestMethod]
        public void TypeConvert_EnumNull()
        {
            var value = TypeConvert.ToType("OrdinalIgnoreCase", typeof(StringComparison?));
            Assert.IsInstanceOfType(value, typeof(StringComparison));
            Assert.AreEqual(value, StringComparison.OrdinalIgnoreCase);
        }

        [TestMethod]
        public void TypeConvert_IntEnumActualNull()
        {
            var value = TypeConvert.ToType("", typeof(StringComparison?));
            Assert.IsNull(value);
        }

        [TestMethod]
        public void TypeConvert_Impl_EnumNull()
        {
            var value = TypeConvert.To<StringComparison?>("OrdinalIgnoreCase");
            Assert.IsInstanceOfType(value, typeof(StringComparison));
            Assert.AreEqual(value, StringComparison.OrdinalIgnoreCase);
        }

        [TestMethod]
        public void TypeConvert_Impl_EnumActualNull()
        {
            var value = TypeConvert.To<StringComparison?>("");
            Assert.IsNull(value);
        }

        [TestMethod]
        public void TypeConvert_Bool()
        {
            var value = TypeConvert.To<Boolean>("true");
            Assert.IsInstanceOfType(value, typeof(Boolean));
            Assert.AreEqual(value, true);
        }

        [TestMethod]
        public void TypeConvert_Guid()
        {
            var guid = Guid.NewGuid();
            var value = TypeConvert.To<Guid>(guid.ToString());
            Assert.IsInstanceOfType(value, typeof(Guid));
            Assert.AreEqual(value, guid);
        }

        [TestMethod]
        public void TypeConvert_GuidNull()
        {
            var guid = Guid.NewGuid();
            var value = TypeConvert.To<Guid?>(guid.ToString());
            Assert.IsInstanceOfType(value, typeof(Guid));
            Assert.AreEqual(value, guid);
        }

        [TestMethod]
        public void TypeConvert_GuidActualNull()
        {
            var value = TypeConvert.To<Guid?>("");
            Assert.IsNull(value);
        }

        [TestMethod]
        public void TypeConvert_DateTime()
        {
            var dateStr = DateTime.Now.ToString();
            var date = DateTime.Parse(dateStr);
            var value = TypeConvert.To<DateTime>(dateStr);
            Assert.IsInstanceOfType(value, typeof(DateTime));
            Assert.AreEqual(value, date);
        }

        [TestMethod]
        public void TypeConvert_DateTime_Universal()
        {
            var dateStr = "2012-05-18";
            var date = DateTime.Parse(dateStr);
            var value = TypeConvert.To<DateTime>(dateStr);
            Assert.IsInstanceOfType(value, typeof(DateTime));
            Assert.AreEqual(value, date);
        }

        [TestMethod]
        public void TypeConvert_DateTime_Regular()
        {
            var dateStr = "1/2/2012 12:30";
            var date = DateTime.Parse(dateStr);
            var value = TypeConvert.To<DateTime>(dateStr);
            Assert.IsInstanceOfType(value, typeof(DateTime));
            Assert.AreEqual(value, date);
        }

        [TestMethod]
        public void TypeConvert_DateTime_Date()
        {
            var dateStr = "1/2/2012 12:30";
            var date = DateTime.Parse(dateStr);
            var value = TypeConvert.To<DateTime>(dateStr);
            Assert.IsInstanceOfType(value, typeof(DateTime));
            Assert.AreEqual(value, date);
        }

        [TestMethod]
        public void TypeConvert_DateTimeNull()
        {
            var dateStr = DateTime.Now.ToString();
            var date = DateTime.Parse(dateStr);
            var value = TypeConvert.To<DateTime?>(dateStr);
            Assert.IsInstanceOfType(value, typeof(DateTime));
            Assert.AreEqual(value, date);
        }

        [TestMethod]
        public void TypeConvert_DateTimeActualNull()
        {
            var value = TypeConvert.To<DateTime?>("");
            Assert.IsNull(value);
        }
    }
}
