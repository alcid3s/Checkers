using Checkers.board;
using Checkers.pieces;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestEmptyFen()
        {
            Board board = new(false);
            //Test empty board
            board.Init("10/10/10/10/10/10/10/10/10/10");
            Assert.IsTrue(board.Tiles.All(x => x.Piece == null));
        }

        [TestMethod]
        public void TestSampleFen()
        {
            Board board = new(false);
            //Test board with black and white pieces
            board.Init("9p/10/10/10/10/10/10/10/10/P9");
            Assert.IsTrue(board.Tiles.All(x =>
            (x.GetPositionInTilesArray() == 9 && x.Piece?.SideOfPiece == Piece.Side.Black) ^
            (x.GetPositionInTilesArray() == 90 && x.Piece?.SideOfPiece == Piece.Side.White) ^
            (x.Piece == null)));
        }
    }
}