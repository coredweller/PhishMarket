using TheCore.Guess;

namespace PhishPond.Concrete
{
    public partial class Topic : ITopic
    {
        public TopicType TopicType { get { return (TopicType)this.Type; } }
    }
}
