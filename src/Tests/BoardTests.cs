using NUnit.Framework;
using Objects.Src;

namespace Tests
{
    internal class BoardTests : BaseTests
    {
        private ChessBoard boardInitial;

        [SetUp]
        public void Setup()
        {
            boardInitial = ChessBoard.CreateInitial();
        }


        [Test]
        [Description("Check initial chess board state")]
        public void Board_InitializeDefault()
        {
            Assert.Multiple(() =>
            {
                Assert.That(boardInitial.GetField("a1").IsWhite, Is.False, "A1 is black field");
                Assert.That(boardInitial.GetField("a1").HasFigure, Is.True, "A1 has rock");
                Assert.That(boardInitial.GetField("e4").IsWhite, Is.True, "E4 is white field");
                Assert.That(boardInitial.GetField("e4").HasFigure, Is.False, "E4 has not any figure");
            });
        }

        [TestCase("e2-e4", FigureType.Pawn)]
        [TestCase("e2-e3", FigureType.Pawn)]
        [TestCase("c7-c5", FigureType.Pawn)]
        [TestCase("c7-c6", FigureType.Pawn)]
        [TestCase("b1-c3", FigureType.Knight)]
        [TestCase("b1-a3", FigureType.Knight)]
        [TestCase("g8-f6", FigureType.Knight)]
        [TestCase("b8-c6", FigureType.Knight)]
        [Description("Make simple moves avaliable from initial position")]
        public void Move_Valid_CheckBoard(string move, FigureType figureExpected)
        {
            var moveItems = move.Split('-'); 

            var isSuccess = boardInitial.MakeMove(move);

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.True, "Move was performed successfully");
                Assert.That(boardInitial.GetField(moveItems[0]).HasFigure, Is.False, "Pawn moved, 'field from' is free now");
                Assert.That(boardInitial.GetField(moveItems[0]).Figure, Is.Null);
                Assert.That(boardInitial.GetField(moveItems[1]).HasFigure, Is.True, "Pawn moved, 'field to' is set");
                Assert.That(boardInitial.GetField(moveItems[1]).Figure.Type, Is.EqualTo(figureExpected), "Figure");
            });
        }

        [TestCase("e2-d3")]
        [TestCase("e2-e5")]
        [TestCase("e2-e1")]
        [Description("Validate pawns moves")]
        public void Move_InvalidPawnMove(string move)
        {
            var moveItems = move.Split('-');

            var fieldInitial = boardInitial.GetField(moveItems[0]);
            var fieldAfter = boardInitial.GetField(moveItems[1]);

            var isSuccess = boardInitial.MakeMove(move);

            var fieldNow = boardInitial.GetField(moveItems[0]);
            var fieldAfterNow = boardInitial.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.False, "Move failed");
                Assert.That(fieldInitial.Figure, Is.EqualTo(fieldNow.Figure), "No affection");
                Assert.That(fieldAfterNow.Figure, Is.EqualTo(fieldAfter.Figure), "No affection");
            });
        }

        [Test]
        [Description("Simple valid pawn capture")]
        public void Move_PawnCapture_Success()
        {
            var figure = boardInitial.GetField("e2").Figure;
            boardInitial.MakeMove("e2-e4");
            boardInitial.MakeMove("d7-d5");
            var isSuccess = boardInitial.MakeMove("e4xd5");
            var figureNow = boardInitial.GetField("d5").Figure;

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.True, "Move processed");
                Assert.That(figureNow.Type, Is.EqualTo(figure.Type));
                Assert.That(figureNow.IsWhitePeace, Is.EqualTo(figure.IsWhitePeace), "Color matched");
            });
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
            var board = ChessBoard.CreateOnlyBoard();

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
            var board = ChessBoard.CreateOnlyBoard();
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
            var board = ChessBoard.CreateOnlyBoard();

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
    }
}
