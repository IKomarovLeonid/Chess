﻿using NUnit.Framework;
using Objects.Src;

namespace Tests
{
    internal class BoardTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            board = ChessBoard.CreateInitial();
        }


        [Test]
        [Description("Check initial chess board state")]
        public void Board_InitializeDefault()
        {
            Assert.Multiple(() =>
            {
                Assert.That(board.GetField("a1").IsWhiteField, Is.False, "A1 is black field");
                Assert.That(board.GetField("a1").HasFigure, Is.True, "A1 has rock");
                Assert.That(board.GetField("e4").IsWhiteField, Is.True, "E4 is white field");
                Assert.That(board.GetField("e4").HasFigure, Is.False, "E4 has not any figure");
                Assert.That(board.GetField("d6").HasFigure, Is.False, "D6 has not any figure");
                Assert.That(board.GetField("g5").HasFigure, Is.False, "G5 has not any figure");
                Assert.That(board.GetField("a8").IsWhiteField, Is.True, "A8 is white field");
                Assert.That(board.GetField("d7").HasFigure, Is.True, "d7 has pawn");
                Assert.That(board.GetField("e8").HasFigure, Is.True, "E8 has king");
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

            var isSuccess = board.MakeMove(move);

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.True, "Move was performed successfully");
                Assert.That(board.GetField(moveItems[0]).HasFigure, Is.False, "Pawn moved, 'field from' is free now");
                Assert.That(board.GetField(moveItems[0]).Figure, Is.Null);
                Assert.That(board.GetField(moveItems[1]).HasFigure, Is.True, "Pawn moved, 'field to' is set");
                Assert.That(board.GetField(moveItems[1]).Figure.GetFigureType(), Is.EqualTo(figureExpected), "Figure");
            });
        }

        [TestCase("e2-d3")]
        [TestCase("e2-e5")]
        [TestCase("e2-e1")]
        [Description("Validate intial pawns moves")]
        public void Board_InvalidPawnMove(string move)
        {
            var moveItems = move.Split('-');

            var fieldInitial = board.GetField(moveItems[0]);
            var fieldAfter = board.GetField(moveItems[1]);

            var isSuccess = board.MakeMove(move);

            var fieldNow = board.GetField(moveItems[0]);
            var fieldAfterNow = board.GetField(moveItems[1]);

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
            var figure = board.GetField("e2").Figure;
            board.MakeMove("e2-e4");
            board.MakeMove("d7-d5");
            var isSuccess = board.MakeMove("e4xd5");
            var figureNow = board.GetField("d5").Figure;

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
                board.MakeMove("e2-e4");
                board.MakeMove("e7-e5");
                board.MakeMove("g1-f3");
                board.MakeMove("b8-c6");
                board.MakeMove("f1-c4");
                board.MakeMove("d7-d6");
            }
            else
            {
                board.MakeMove("e2-e4");
                board.MakeMove("e7-e5");
                board.MakeMove("g1-f3");
                board.MakeMove("g8-f6");
                board.MakeMove("f1-c4");
                board.MakeMove("f8-c5");
                board.MakeMove("0-0");
            }

            // act
            var result = board.MakeMove(move);
            var row = whitePeacesScenario ? "1" : "8";
            // assert
            var g = board.GetField($"g{row}");
            var f = board.GetField($"f{row}");
            var e = board.GetField($"e{row}");
            var h = board.GetField($"h{row}");

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
                board.MakeMove("d2-d4");
                board.MakeMove("d7-d5");
                board.MakeMove("b1-c3");
                board.MakeMove("b8-c6");
                board.MakeMove("c1-f4");
                board.MakeMove("e7-e6");
                board.MakeMove("d1-d2");
                board.MakeMove("g8-f6");
            }
            else
            {
                board.MakeMove("d2-d4");
                board.MakeMove("d7-d5");
                board.MakeMove("b1-c3");
                board.MakeMove("b8-c6");
                board.MakeMove("c1-f4");
                board.MakeMove("c8-f5");
                board.MakeMove("d1-d2");
                board.MakeMove("d7-d6");
                board.MakeMove("g2-g3");
            }

            // act
            var result = board.MakeMove(move);
            var row = isWhileScenario ? "1" : "8";
            // assert
            var c = board.GetField($"c{row}");
            var d = board.GetField($"d{row}");
            var e = board.GetField($"e{row}");
            var a = board.GetField($"a{row}");
            var b = board.GetField($"b{row}");

            Assert.Multiple(() =>
            {
                Assert.True(result, "Move processed");
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
