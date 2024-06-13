using NUnit.Framework;
using Objects.Src;

namespace Tests
{
    internal class LogicTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            board = ChessBoard.CreateOnlyBoard();
            moveProcessor = new MoveProcessor(board);
        }

        [Test]
        public void SimpleCheckState()
        {
            board.SetFigure(new Figure(FigureType.Knight, true), "e4");
            board.SetFigure(new Figure(FigureType.King, false), "d6");

            var isCheck = moveProcessor.IsCheckState();

            Assert.IsTrue(isCheck);
        }

        [Test]
        public void CheckState_SameColor_NotACheck()
        {
            board.SetFigure(new Figure(FigureType.Knight, true), "e4");
            board.SetFigure(new Figure(FigureType.King, true), "d6");

            var isCheck = moveProcessor.IsCheckState();

            Assert.False(isCheck);
        }

        [Test]
        public void SimpleMateState_Success()
        {
            // check here
            board.SetFigure(new Figure(FigureType.Rook, true), "b8");
            // black peaces
            board.SetFigure(new Figure(FigureType.King, false), "g8");
            board.SetFigure(new Figure(FigureType.Pawn, false), "g7");
            board.SetFigure(new Figure(FigureType.Pawn, false), "f7");
            board.SetFigure(new Figure(FigureType.Pawn, false), "h7");

            var isCheck = moveProcessor.IsCheckState();
            var isMate = moveProcessor.IsMateState();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(isCheck);
                Assert.IsTrue(isMate);
            });
        }

        [Test]
        public void SimpleDrawState_Success()
        {
            // check here
            board.SetFigure(new Figure(FigureType.King, true), "b8");
            // black peaces
            board.SetFigure(new Figure(FigureType.King, false), "g8");

            var isCheck = moveProcessor.IsCheckState();
            var isMate = moveProcessor.IsMateState();
            var isDraw = moveProcessor.IsDraw();

            Assert.Multiple(() =>
            {
                Assert.IsFalse(isCheck);
                Assert.IsFalse(isMate);
                Assert.IsTrue(isDraw);
            });
        }

        [Test]
        public void NotMate_CanCover()
        {
            // check here
            board.SetFigure(new Figure(FigureType.Rook, true), "b8");
            // black peaces
            board.SetFigure(new Figure(FigureType.King, false), "g8");
            board.SetFigure(new Figure(FigureType.Pawn, false), "g7");
            board.SetFigure(new Figure(FigureType.Pawn, false), "f7");
            board.SetFigure(new Figure(FigureType.Pawn, false), "h7");
            board.SetFigure(new Figure(FigureType.Knight, false), "g6"); // f8 -> not mate

            var isCheck = moveProcessor.IsCheckState();
            var isMate = moveProcessor.IsMateState();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(isCheck);
                Assert.IsFalse(isMate);
            });
        }
    }
}
