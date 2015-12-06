using FlexLabs.Linq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FlexLabs.Util.Tests.Linq
{
    public class OrderByExpressionTests
    {
        class ModelClass
        {
            public ModelClass(int id, string firstName, string lastName)
            {
                ID = id;
                FirstName = firstName;
                LastName = lastName;
            }

            public int ID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        enum ModelSorter { ID, Name, FirstLast, LastFirst }

        private static OrderByExpressionCollection<ModelSorter, ModelClass> _sorter = new OrderByExpressionCollection<ModelSorter, ModelClass>
        {
            { ModelSorter.ID, m => m.ID },
            { ModelSorter.Name, m => m.FirstName },
            { ModelSorter.FirstLast, m => m.FirstName, m => m.LastName },
            { ModelSorter.LastFirst, m => m.LastName, m => m.FirstName },
        };

        private static List<ModelClass> _sample = new List<ModelClass>
        {
            new ModelClass(1, "5", "c"),
            new ModelClass(2, "3", "e"),
            new ModelClass(3, "3", "d"),
            new ModelClass(4, "2", "b"),
            new ModelClass(5, "1", "a"),
        };

        [Fact]
        public void OrderByExpression_OrderInt_Asc()
        {
            var sortedModel = _sorter[ModelSorter.ID].ApplyOrdering(_sample.AsQueryable(), true).ToList();
            Assert.Equal(_sample.Count, sortedModel.Count);
            Assert.Equal(1, sortedModel[0].ID);
            Assert.Equal(5, sortedModel[4].ID);
        }

        [Fact]
        public void OrderByExpression_OrderInt_Desc()
        {
            var sortedModel = _sorter[ModelSorter.ID].ApplyOrdering(_sample.AsQueryable(), false).ToList();
            Assert.Equal(_sample.Count, sortedModel.Count);
            Assert.Equal(5, sortedModel[0].ID);
            Assert.Equal(1, sortedModel[4].ID);
        }

        [Fact]
        public void OrderByExpression_OrderString_Asc()
        {
            var sortedModel = _sorter[ModelSorter.Name].ApplyOrdering(_sample.AsQueryable(), true).ToList();
            Assert.Equal(_sample.Count, sortedModel.Count);
            Assert.Equal(5, sortedModel[0].ID);
            Assert.Equal(1, sortedModel[4].ID);
        }

        [Fact]
        public void OrderByExpression_OrderString_Desc()
        {
            var sortedModel = _sorter[ModelSorter.Name].ApplyOrdering(_sample.AsQueryable(), false).ToList();
            Assert.Equal(_sample.Count, sortedModel.Count);
            Assert.Equal(1, sortedModel[0].ID);
            Assert.Equal(5, sortedModel[4].ID);
        }

        [Fact]
        public void OrderByExpression_OrderMulti1_Asc()
        {
            var sortedModel = _sorter[ModelSorter.FirstLast].ApplyOrdering(_sample.AsQueryable(), true).ToList();
            Assert.Equal(_sample.Count, sortedModel.Count);
            Assert.Equal(5, sortedModel[0].ID);
            Assert.Equal(3, sortedModel[2].ID);
            Assert.Equal(2, sortedModel[3].ID);
            Assert.Equal(1, sortedModel[4].ID);
        }

        [Fact]
        public void OrderByExpression_OrderMulti1_Desc()
        {
            var sortedModel = _sorter[ModelSorter.FirstLast].ApplyOrdering(_sample.AsQueryable(), false).ToList();
            Assert.Equal(_sample.Count, sortedModel.Count);
            Assert.Equal(1, sortedModel[0].ID);
            Assert.Equal(2, sortedModel[1].ID);
            Assert.Equal(3, sortedModel[2].ID);
            Assert.Equal(5, sortedModel[4].ID);
        }

        [Fact]
        public void OrderByExpression_OrderMulti2_Asc()
        {
            var sortedModel = _sorter[ModelSorter.LastFirst].ApplyOrdering(_sample.AsQueryable(), true).ToList();
            Assert.Equal(_sample.Count, sortedModel.Count);
            Assert.Equal(5, sortedModel[0].ID);
            Assert.Equal(4, sortedModel[1].ID);
            Assert.Equal(3, sortedModel[3].ID);
            Assert.Equal(2, sortedModel[4].ID);
            Assert.Equal(1, sortedModel[2].ID);
        }

        [Fact]
        public void OrderByExpression_OrderMulti2_Desc()
        {
            var sortedModel = _sorter[ModelSorter.LastFirst].ApplyOrdering(_sample.AsQueryable(), false).ToList();
            Assert.Equal(_sample.Count, sortedModel.Count);
            Assert.Equal(1, sortedModel[2].ID);
            Assert.Equal(2, sortedModel[0].ID);
            Assert.Equal(3, sortedModel[1].ID);
            Assert.Equal(4, sortedModel[3].ID);
            Assert.Equal(5, sortedModel[4].ID);
        }
    }
}
