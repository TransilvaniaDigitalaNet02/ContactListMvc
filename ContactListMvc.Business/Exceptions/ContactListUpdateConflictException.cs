using ContactListMvc.Business.Models;

namespace ContactListMvc.Business.Exceptions
{
    public sealed class ContactListUpdateConflictException : BaseException
    {
        public ContactListUpdateConflictException(
            bool entryDeletedInTheMeanwhile, Exception innerException)
            : base("Cannot update the specified contact list entry because operation conflicts with another concurrent update.", innerException)
        {
            EntryDeletedInTheMeanwhile = entryDeletedInTheMeanwhile;
        }

        public bool EntryDeletedInTheMeanwhile { get; }

        public override ErrorType ErrorType => ErrorType.CurrentStateDoesntAllowChangeError;
    }
}
