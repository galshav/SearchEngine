using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace App.UnitTests {
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Index_CheckRegularBook_ShouldIndexAsExpected()
        {
            var book = new List<string>()
            {
                "This is sentence #1",
                "This is sentence #2",
            };
            
            var index = Framework.Index.CreateIndex(data: book);
            Assert.AreEqual(expected: 1, actual: index["#1"].Count);
            Assert.AreEqual(expected: 1, actual: index["#2"].Count);
            Assert.AreEqual(expected: 2, actual: index["This"].Count);
            Assert.AreEqual(expected: 2, actual: index["is"].Count);
            Assert.AreEqual(expected: 2, actual: index["is"].Count);
            Assert.AreEqual(expected: 2, actual: index["sentence"].Count);
            Assert.AreEqual(expected: 0, actual: index["#1"][0]);
            Assert.AreEqual(expected: 1, actual: index["#2"][0]);
            Assert.AreEqual(expected: 0, actual: index["This"][0]);
            Assert.AreEqual(expected: 1, actual: index["This"][1]);
            Assert.AreEqual(expected: 0, actual: index["is"][0]);
            Assert.AreEqual(expected: 1, actual: index["is"][1]);
            Assert.AreEqual(expected: 0, actual: index["sentence"][0]);
            Assert.AreEqual(expected: 1, actual: index["sentence"][1]);
        }

        [TestMethod]
        public void ExpressionTree_BuildSimpleExpression_TodayANDSunday()
        {
            var expressionTree = new Framework.ExpressionTree(
                new Framework.Node()
                {
                    Data  = "&&",
                    Left  = new Framework.Node() { Data = "Today"},
                    Right = new Framework.Node() { Data = "Sunday"}
                });
            
            Assert.AreEqual(expected: "Today" , actual: expressionTree.Root.Left.Data);
            Assert.AreEqual(expected: "Sunday", actual: expressionTree.Root.Right.Data);
            Assert.AreEqual(expected: "&&"    , actual: expressionTree.Root.Data);
        }

        [TestMethod]
        public void ExpressionTree_BuildComplexExpression_TodayANDSunday_OR_NotORTomorow()
        {
            var leftSubExpression = new Framework.Node()
            {
                Data  = "&&",
                Left  = new Framework.Node() { Data = "Today" },
                Right = new Framework.Node() { Data = "Sunday" },
            };

            var rightSubExpression = new Framework.Node()
            {
                Data  = "||",
                Left  = new Framework.Node() { Data = "Not" },
                Right = new Framework.Node() { Data = "Tomorrow" },
            };

            var expression = new Framework.ExpressionTree(
                new Framework.Node()
                {
                    Data  = "||",
                    Left  = leftSubExpression,
                    Right = rightSubExpression,
                });

            Assert.AreEqual(expected: "||"      , actual: expression.Root.Data);
            Assert.AreEqual(expected: "||"      , actual: expression.Root.Right.Data);
            Assert.AreEqual(expected: "&&"      , actual: expression.Root.Left.Data);
            Assert.AreEqual(expected: "Today"   , actual: expression.Root.Left.Left.Data);
            Assert.AreEqual(expected: "Sunday"  , actual: expression.Root.Left.Right.Data);
            Assert.AreEqual(expected: "Not"     , actual: expression.Root.Right.Left.Data);
            Assert.AreEqual(expected: "Tomorrow", actual: expression.Root.Right.Right.Data);
        }

        [TestMethod]
        public void SearchEngine_SearchExpression_SingleWord()
        {
            var searchFilter = new Framework.ExpressionTree(
                new Framework.Node()
                {
                    Data  = "Sunday",
                    Left  = null,
                    Right = null,
                });

            var dataset = new List<string>()
            {
                "Today is Sunday",
                "Today is not Monday",
                "Tomorrow is Tuesday",
                "Tomorrow isn't Wednesday"
            };

            var searchEngine = new Framework.SearchEngine(dataset: dataset);
            var indices = searchEngine.Search(searchFilter: searchFilter.Root);
            Assert.AreEqual(expected: 1, actual: indices.Count);
            Assert.AreEqual(expected: 0, actual: indices[0]);
        }

        [TestMethod]
        public void SearchEngine_SearchExpression_TodayANDSunday()
        {
            var searchFilter = new Framework.Node()
            {
                Data  = "&&",
                Left  = new Framework.Node() { Data = "Today" },
                Right = new Framework.Node() { Data = "Sunday" },
            };

            var dataset = new List<string>()
            {
                "Today is Sunday",
                "Today is not Monday",
                "Tomorrow is Tuesday",
                "Tomorrow isn't Wednesday"
            };

            var searchEngine = new Framework.SearchEngine(dataset: dataset);
            var indices = searchEngine.Search(searchFilter: searchFilter);
            Assert.AreEqual(expected: 1, actual: indices.Count);
            Assert.AreEqual(expected: 0, actual: indices[0]);
        }

        [TestMethod]
        public void SearchEngine_SearchExpression_TodayORTomorrow()
        {
            var searchFilter = new Framework.Node()
            {
                Data = "||",
                Left = new Framework.Node() { Data = "Today" },
                Right = new Framework.Node() { Data = "Tomorrow" },
            };

            var dataset = new List<string>()
            {
                "Today is Sunday",
                "Today is not Monday",
                "Tomorrow is Tuesday",
                "Tomorrow isn't Wednesday"
            };

            var searchEngine = new Framework.SearchEngine(dataset: dataset);
            var indices = searchEngine.Search(searchFilter: searchFilter);
            Assert.AreEqual(expected: 4, actual: indices.Count);
            Assert.AreEqual(expected: 0, actual: indices[0]);
            Assert.AreEqual(expected: 1, actual: indices[1]);
            Assert.AreEqual(expected: 2, actual: indices[2]);
            Assert.AreEqual(expected: 3, actual: indices[3]);
        }

        [TestMethod]
        public void SearchEngine_SearchExpression_TodayANDSunday_OR_NotORTomorow()
        {
            var leftSubExpression = new Framework.Node()
            {
                Data = "&&",
                Left = new Framework.Node() { Data = "Today" },
                Right = new Framework.Node() { Data = "Sunday" },
            };

            var rightSubExpression = new Framework.Node()
            {
                Data = "||",
                Left = new Framework.Node() { Data = "not" },
                Right = new Framework.Node() { Data = "Tomorrow" },
            };

            var searchFilter = new Framework.Node()
            {
                Data = "||",
                Left = leftSubExpression,
                Right = rightSubExpression,
            };

            var dataset = new List<string>()
            {
                "Today is Sunday",
                "Today is not Monday",
                "Tomorrow is Tuesday",
                "Tomorrow isn't Wednesday"
            };

            var searchEngine = new Framework.SearchEngine(dataset: dataset);
            var indices = searchEngine.Search(searchFilter: searchFilter);
            Assert.AreEqual(expected: 4, actual: indices.Count);
            Assert.AreEqual(expected: 0, actual: indices[0]);
            Assert.AreEqual(expected: 1, actual: indices[1]);
            Assert.AreEqual(expected: 2, actual: indices[2]);
            Assert.AreEqual(expected: 3, actual: indices[3]);
        }
    }
}
