using NUnit.Framework;
using Objects;

namespace Tests
{
    internal class BoardTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            Board = ChessBoard.CreateInitial();
            MoveProcessor = new MoveProcessor(Board);
        }


        [Test]
        [Description("Check initial chess board state")]
        public void Board_InitializeDefault()
        {
            Assert.Multiple(() =>
            {
                Assert.That(Board.GetField("a1").IsWhiteField, Is.False, "A1 is black field");
                Assert.That(Board.GetField("a1").HasFigure, Is.True, "A1 has rock");
                Assert.That(Board.GetField("e4").IsWhiteField, Is.True, "E4 is white field");
                Assert.That(Board.GetField("e4").HasFigure, Is.False, "E4 has not any figure");
                Assert.That(Board.GetField("d6").HasFigure, Is.False, "D6 has not any figure");
                Assert.That(Board.GetField("g5").HasFigure, Is.False, "G5 has not any figure");
                Assert.That(Board.GetField("a8").IsWhiteField, Is.True, "A8 is white field");
                Assert.That(Board.GetField("d7").HasFigure, Is.True, "d7 has pawn");
                Assert.That(Board.GetField("e8").HasFigure, Is.True, "E8 has king");
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
        public void Board_Valid_CheckBoard(string move, FigureType figureExpected)
        {
            var moveItems = move.Split('-'); 

            var isSuccess = MoveProcessor.MakeMove(move);

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.True, "Move was performed successfully");
                Assert.That(Board.GetField(moveItems[0]).HasFigure, Is.False, "Pawn moved, 'field from' is free now");
                Assert.That(Board.GetField(moveItems[0]).Figure, Is.Null);
                Assert.That(Board.GetField(moveItems[1]).HasFigure, Is.True, "Pawn moved, 'field to' is set");
                Assert.That(Board.GetField(moveItems[1]).Figure.GetFigureType(), Is.EqualTo(figureExpected), "Figure");
            });
        }

        [TestCase("e2-d3")]
        [TestCase("e2-e5")]
        [TestCase("e2-e1")]
        [Description("Validate intial pawns moves")]
        public void Board_InvalidPawnMove(string move)
        {
            var moveItems = move.Split('-');

            var fieldInitial = Board.GetField(moveItems[0]);
            var fieldAfter = Board.GetField(moveItems[1]);

            var isSuccess = MoveProcessor.MakeMove(move);

            var fieldNow = Board.GetField(moveItems[0]);
            var fieldAfterNow = Board.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.False, "Move failed");
                Assert.That(fieldInitial.Figure, Is.EqualTo(fieldNow.Figure), "No affection");
                Assert.That(fieldAfterNow.Figure, Is.EqualTo(fieldAfter.Figure), "No affection");
            });
        }

        [Test]
        [Description("Simple valid pawn capture from initial position")]
        public void Board_PawnCapture_Success()
        {
            var figure = Board.GetField("e2").Figure;
            MoveProcessor.MakeMove("e2-e4");
            MoveProcessor.MakeMove("d7-d5");
            var isSuccess = MoveProcessor.MakeMove("e4xd5");
            var figureNow = Board.GetField("d5").Figure;

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.True, "Move processed");
                Assert.That(figureNow.GetFigureType(), Is.EqualTo(figure.GetFigureType()));
                Assert.That(figureNow.IsWhitePeace, Is.EqualTo(figure.IsWhitePeace), "Color matched");
            });
        }

        [TestCase("0-0", true)]
        [TestCase("0-0", false)]
        [Description("Perform castle king side valid")]
        public void Board_Castle_KingSide(string move, bool whitePeacesScenario)
        {
            if (whitePeacesScenario)
            {
                MoveProcessor.MakeMove("e2-e4");
                MoveProcessor.MakeMove("e7-e5");
                MoveProcessor.MakeMove("g1-f3");
                MoveProcessor.MakeMove("b8-c6");
                MoveProcessor.MakeMove("f1-c4");
                MoveProcessor.MakeMove("d7-d6");
            }
            else
            {
                MoveProcessor.MakeMove("e2-e4");
                MoveProcessor.MakeMove("e7-e5");
                MoveProcessor.MakeMove("g1-f3");
                MoveProcessor.MakeMove("g8-f6");
                MoveProcessor.MakeMove("f1-c4");
                MoveProcessor.MakeMove("f8-c5");
                MoveProcessor.MakeMove("0-0");
            }

            // act
            var result = MoveProcessor.MakeMove(move);
            var row = whitePeacesScenario ? "1" : "8";
            // assert
            var g = Board.GetField($"g{row}");
            var f = Board.GetField($"f{row}");
            var e = Board.GetField($"e{row}");
            var h = Board.GetField($"h{row}");

            Assert.Multiple(() =>
            {
                Assert.True(result, "Move processed");
                Assert.True(g.HasFigure());
                Assert.True(f.HasFigure());
                Assert.False(e.HasFigure());
                Assert.False(h.HasFigure());
                Assert.That(g.Figure.GetFigureType(), Is.EqualTo(FigureType.King));
                Assert.That(f.Figure.GetFigureType(), Is.EqualTo(FigureType.Rook));
            });
        }

        [TestCase("0-0-0", true)]
        [TestCase("0-0-0", false)]
        [Description("Perform castle queen side valid")]
        public void Board_Castle_QueenSide(string move, bool isWhileScenario)
        {
            if (isWhileScenario)
            {
                MoveProcessor.MakeMove("d2-d4");
                MoveProcessor.MakeMove("d7-d5");
                MoveProcessor.MakeMove("b1-c3");
                MoveProcessor.MakeMove("b8-c6");
                MoveProcessor.MakeMove("c1-f4");
                MoveProcessor.MakeMove("e7-e6");
                MoveProcessor.MakeMove("d1-d2");
                MoveProcessor.MakeMove("g8-f6");
            }
            else
            {
                MoveProcessor.MakeMove("d2-d4");
                MoveProcessor.MakeMove("d7-d5");
                MoveProcessor.MakeMove("b1-c3");
                MoveProcessor.MakeMove("b8-c6");
                MoveProcessor.MakeMove("c1-f4");
                MoveProcessor.MakeMove("c8-f5");
                MoveProcessor.MakeMove("d1-d2");
                MoveProcessor.MakeMove("d8-d7");
                MoveProcessor.MakeMove("g2-g3");
            }

            // act
            var result = MoveProcessor.MakeMove(move);
            var row = isWhileScenario ? "1" : "8";
            // assert
            var c = Board.GetField($"c{row}");
            var d = Board.GetField($"d{row}");
            var e = Board.GetField($"e{row}");
            var a = Board.GetField($"a{row}");
            var b = Board.GetField($"b{row}");

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.True, "Move processed");
                Assert.True(c.HasFigure());
                Assert.True(d.HasFigure());
                Assert.False(e.HasFigure());
                Assert.False(a.HasFigure());
                Assert.False(b.HasFigure());
                Assert.That(c.Figure.GetFigureType(), Is.EqualTo(FigureType.King));
                Assert.That(d.Figure.GetFigureType(), Is.EqualTo(FigureType.Rook));
            });
        }

    }
}
