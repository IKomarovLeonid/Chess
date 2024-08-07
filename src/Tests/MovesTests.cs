﻿using NUnit.Framework;
using Objects;

namespace Tests
{
    internal class MovesTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            Board = ChessBoard.CreateOnlyBoard();
            MoveProcessor = new MoveProcessor(Board);
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

            Board.SetFigure(new Figure(FigureType.Knight, true), moveItems[0]);

            var result = MoveProcessor.MakeMove(move);

            var fieldInitialAfterMove = Board.GetField(moveItems[0]);
            var fieldAfterMove = Board.GetField(moveItems[1]);

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
            Board.SetFigure(new Figure(FigureType.Bishop, true), moveItems[0]);

            var result = MoveProcessor.MakeMove(move);

            var fieldInitialAfterMove = Board.GetField(moveItems[0]);
            var fieldAfterMove = Board.GetField(moveItems[1]);

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

            Board.SetFigure(new Figure(FigureType.Knight, true), moveItems[0]);

            var result = MoveProcessor.MakeMove(move);

            var fieldInitialAfterMove = Board.GetField(moveItems[0]);
            var fieldAfterMove = Board.GetField(moveItems[1]);

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
            Board.SetFigure(new Figure(FigureType.Rook, true), moveItems[0]);

            var result = MoveProcessor.MakeMove(move);

            var fieldInitialAfterMove = Board.GetField(moveItems[0]);
            var fieldAfterMove = Board.GetField(moveItems[1]);

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
            Board.SetFigure(new Figure(FigureType.Queen, true), moveItems[0]);

            var result = MoveProcessor.MakeMove(move);

            var fieldInitialAfterMove = Board.GetField(moveItems[0]);
            var fieldAfterMove = Board.GetField(moveItems[1]);

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
            Board.SetFigure(new Figure(FigureType.King, true), moveItems[0]);

            var result = MoveProcessor.MakeMove(move);

            var fieldInitialAfterMove = Board.GetField(moveItems[0]);
            var fieldAfterMove = Board.GetField(moveItems[1]);

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
            Board.SetFigure(new Figure(FigureType.Pawn, true), "d3");
            Board.SetFigure(new Figure(FigureType.Pawn, false), "d4");

            var result = MoveProcessor.MakeMove("d3-d4");

            var d3 = Board.GetField("d3");
            var d4 = Board.GetField("d4");

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

        [TestCase("e4-e5")]
        [TestCase("e4-d5")]
        [TestCase("e4-f5")]
        [TestCase("e6-f5")]
        [TestCase("e6-d5")]
        [TestCase("e6-e5")]
        [Description("Success king move hovewer can't be processed if other king after move becomes closer")]
        public void Move_King_NearOtherKing_Failed(string move)
        {
            var moveItems = move.Split('-');
            Board.SetFigure(new Figure(FigureType.King, true), "e4");
            Board.SetFigure(new Figure(FigureType.King, false), "e6");

            var result = MoveProcessor.MakeMove(move);

            var fieldInitialAfterMove = Board.GetField(moveItems[0]);
            var fieldAfterMove = Board.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.That(result, Is.False, "Move was NOT processed");
                Assert.That(fieldInitialAfterMove.HasFigure(), Is.True, "Initial field is not EMPTY after move");
                Assert.That(fieldAfterMove.HasFigure(), Is.False, "Target field is empty after move");
            });
        }

        [TestCase("e7-e8", true)]
        [TestCase("e2-e1", false)]
        [Description("Promote pawn to queen when last rank reached")]
        public void Move_Pawn_LastRank(string move, bool isWhitePawn)
        {
            var moveItems = move.Split('-');
            Board.SetFigure(new Figure(FigureType.Pawn, isWhitePawn), moveItems[0]);

            var result = MoveProcessor.MakeMove(move);

            var fieldInitialAfterMove = Board.GetField(moveItems[0]);
            var fieldAfterMove = Board.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.True(result, "Move was processed");
                Assert.False(fieldInitialAfterMove.HasFigure(), "Initial field is EMPTY after move");
                Assert.True(fieldAfterMove.HasFigure(), "Target field has figure");
                Assert.True(fieldAfterMove.Figure.IsQueen(), "Pawn becomes quenn");
                Assert.That(fieldAfterMove.Figure.IsWhitePeace, Is.EqualTo(isWhitePawn), "Queen has color same as pawn");
            });
        }
    }
}
