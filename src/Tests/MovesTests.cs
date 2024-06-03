using NUnit.Framework;
using Objects.Src;

namespace Tests
{
    internal class MovesTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            board = ChessBoard.CreateOnlyBoard();
        }


        [TestCase("d4-f5")]
        [TestCase("a1-b3")]
        [TestCase("a1-c2")]
        [TestCase("d4-e6")]
        [TestCase("d4-e2")]
        [TestCase("d4-c2")]
        [TestCase("d4-b5")]
        [TestCase("d4-c6")]
        [TestCase("d4-c2")]
        [TestCase("h3-g5")]
        [TestCase("h3-f4")]
        [TestCase("h3-f2")]
        [TestCase("h3-g1")]
        [Description("Perform success knight move")]
        public void Move_Knight_Success(string move)
        {
            var moveItems = move.Split('-');

            board.SetFigure(new Figure(FigureType.Knight, true), moveItems[0]);

            var result = board.MakeMove(move);

            var fieldInitialAfterMove = board.GetField(moveItems[0]);
            var fieldAfterMove = board.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.True(result, "Move was processed");
                Assert.False(fieldInitialAfterMove.HasFigure(), "Initial field is empty after move");
                Assert.True(fieldAfterMove.HasFigure(), "Target field is NOT empty after move");
            });
        }

        [TestCase("a1-h8")]
        [TestCase("f1-c4")]
        [TestCase("g2-b7")]
        [TestCase("f6-d4")]
        [TestCase("c5-f2")]
        [Description("Perform success bishop move")]
        public void Move_Bishop_Success(string move)
        {
            var moveItems = move.Split('-');
            board.SetFigure(new Figure(FigureType.Bishop, true), moveItems[0]);

            var result = board.MakeMove(move);

            var fieldInitialAfterMove = board.GetField(moveItems[0]);
            var fieldAfterMove = board.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.True(result, "Move was processed");
                Assert.False(fieldInitialAfterMove.HasFigure(), "Initial field is empty after move");
                Assert.True(fieldAfterMove.HasFigure(), "Target field is NOT empty after move");
            });
        }

        [TestCase("d4-c5")]
        [TestCase("a2-c2")]
        [TestCase("h1-a8")]
        [Description("Invalid knight move")]
        public void Move_Knight_Invalid(string move)
        {
            var moveItems = move.Split('-');

            board.SetFigure(new Figure(FigureType.Knight, true), moveItems[0]);

            var result = board.MakeMove(move);

            var fieldInitialAfterMove = board.GetField(moveItems[0]);
            var fieldAfterMove = board.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.False(result, "Move failed");
                Assert.True(fieldInitialAfterMove.HasFigure(), "Initial field is same as before move");
                Assert.False(fieldAfterMove.HasFigure(), "Target field is not affected");
            });
        }

        [TestCase("a1-a8")]
        [TestCase("d2-g2")]
        [TestCase("h1-a1")]
        [TestCase("d8-d3")]
        [TestCase("e7-h7")]
        [Description("Perform success rock move")]
        public void Move_Rock_Success(string move)
        {
            var moveItems = move.Split('-');
            board.SetFigure(new Figure(FigureType.Rook, true), moveItems[0]);

            var result = board.MakeMove(move);

            var fieldInitialAfterMove = board.GetField(moveItems[0]);
            var fieldAfterMove = board.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.True(result, "Move was processed");
                Assert.False(fieldInitialAfterMove.HasFigure(), "Initial field is empty after move");
                Assert.True(fieldAfterMove.HasFigure(), "Target field is NOT empty after move");
            });
        }

        [TestCase("a1-a8")]
        [TestCase("d2-g2")]
        [TestCase("h1-a1")]
        [TestCase("d8-d3")]
        [TestCase("e7-h7")]
        [TestCase("c5-e7")]
        [TestCase("b3-a2")]
        [TestCase("h1-c6")]
        [Description("Perform success queen move")]
        public void Move_Queen_Success(string move)
        {
            var moveItems = move.Split('-');
            board.SetFigure(new Figure(FigureType.Queen, true), moveItems[0]);

            var result = board.MakeMove(move);

            var fieldInitialAfterMove = board.GetField(moveItems[0]);
            var fieldAfterMove = board.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.True(result, "Move was processed");
                Assert.False(fieldInitialAfterMove.HasFigure(), "Initial field is empty after move");
                Assert.True(fieldAfterMove.HasFigure(), "Target field is NOT empty after move");
            });
        }

        [TestCase("d2-c1")]
        [TestCase("d2-c2")]
        [TestCase("d2-d3")]
        [TestCase("d2-c3")]
        [TestCase("d2-e2")]
        [TestCase("d2-e1")]
        [TestCase("d2-d1")]
        [TestCase("d2-e3")]
        [Description("Perform success king move")]
        public void Move_King_Success(string move)
        {
            var moveItems = move.Split('-');
            board.SetFigure(new Figure(FigureType.King, true), moveItems[0]);

            var result = board.MakeMove(move);

            var fieldInitialAfterMove = board.GetField(moveItems[0]);
            var fieldAfterMove = board.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.True(result, "Move was processed");
                Assert.False(fieldInitialAfterMove.HasFigure(), "Initial field is empty after move");
                Assert.True(fieldAfterMove.HasFigure(), "Target field is NOT empty after move");
            });
        }

        [Test]
        [Description("Pawn can't be moved -> blockade")]
        public void Move_Pawn_Blocade()
        {
            board.SetFigure(new Figure(FigureType.Pawn, true), "d3");
            board.SetFigure(new Figure(FigureType.Pawn, false), "d4");

            var result = board.MakeMove("d3-d4");

            var d3 = board.GetField("d3");
            var d4 = board.GetField("d4");

            Assert.Multiple(() =>
            {
                Assert.False(result, "Move was NOT processed");
                Assert.True(d3.HasFigure(), "d3 has figure");
                Assert.True(d4.HasFigure(), "d4 has figure");
                Assert.That(d3.Figure.GetFigureType(), Is.EqualTo(FigureType.Pawn), "d3 pawn");
                Assert.That(d4.Figure.GetFigureType(), Is.EqualTo(FigureType.Pawn), "d4 pawn");
                Assert.False(d4.Figure.IsWhitePeace, "d3 pawn white");
                Assert.True(d3.Figure.IsWhitePeace, "d4 pawn white");
            });
        }
    }
}
