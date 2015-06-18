using FlexLabs.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FlexLabs.Util.Tests.Linq
{
    public class OrderByExpressionTests
    {
        class ModelClass
        {
            public ModelClass(int id, string name)
            {
                ID = id;
                Name = name;
            }

            public int ID { get; set; }
            public string Name { get; set; }
        }
        enum ModelSorter { ID, Name }

        private static OrderByExpressionCollection<ModelSorter, ModelClass> _sorter = new OrderByExpressionCollection<ModelSorter, ModelClass>
        {
            { ModelSorter.ID, m => m.ID },
            { ModelSorter.Name, m => m.Name },
        };

        private static List<ModelClass> _sample = new List<ModelClass>
        {
            new ModelClass(1, "5"),
            new ModelClass(2, "4"),
            new ModelClass(3, "3"),
            new ModelClass(4, "2"),
            new ModelClass(5, "1"),
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
    }
}
