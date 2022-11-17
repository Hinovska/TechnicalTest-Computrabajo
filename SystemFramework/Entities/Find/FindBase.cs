namespace Redarbor.SystemFramework.Entities.Find
{
    public class FindBase
    {
        #region Atributes

        private int _PageSize = 20;
        private int _PageNumber = 1;
        private int _Count = 0;

        #endregion

        #region Properties

        public int PageSize { get { return _PageSize; } set { _PageSize = value; } }
        public int PageNumber { get { return _PageNumber; } set { _PageNumber = value; } }
        public int Count { get { return _Count; } set { _Count = value; } }

        #endregion


        #region

        public FindBase()
        {
            this.PageNumber = 10;
            this.PageNumber = 1;
        }

        #endregion
    }
}
