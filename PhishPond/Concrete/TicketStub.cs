using TheCore.Interfaces;

namespace PhishPond.Concrete
{
    public partial class TicketStub : ITicketStub
    {
        #region ITicketStub Members


        #endregion


        public TicketStub(ITicketStub ts)
        {
            this.CreatedDate = ts.CreatedDate;
            this.Deleted = ts.Deleted;
            this.DeletedDate = ts.DeletedDate;
            this.Notes = ts.Notes;
            this.Original = ts.Original;
            this.PhotoId = ts.PhotoId;
            this.ShowId = ts.ShowId;
            this.TicketStubId = ts.TicketStubId;
            this.UpdatedDate = ts.UpdatedDate;
            this.UserId = ts.UserId;
        }
    }
}
