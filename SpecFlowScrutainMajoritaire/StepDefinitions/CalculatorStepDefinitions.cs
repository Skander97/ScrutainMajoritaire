using System.Reflection;
using TechTalk.SpecFlow.CommonModels;

namespace SpecFlowScrutainMajoritaire.StepDefinitions
{
    [Binding]
    public sealed class CalculatorStepDefinitions
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private Dictionary<string, double> _votes;
        private String topCandidates ;
        private String result;


        [Given(@"the vote")]
        public void GivenTheVote(Table table)
        {
            _votes = table.Rows.ToDictionary(row => row["candidate"], row => double.Parse(row["pourcentage"]));
        }

        [Then(@"the (.*) is the winner")]
        public void ThenTheCandidateIsTheWinner(string candidate)
        {
            string actualWinner = _votes.OrderByDescending(vote => vote.Value).First().Key;
            Assert.Equal(candidate, actualWinner);
        }

        [Then(@"the (.*) and the (.*) advance the second round")]
        public void ThenTheCandidate1AndTheCandidate3AdvanceTheSecondRound(string candidate1, string candidate2)
        {
            var topVotes = _votes.OrderByDescending(vote => vote.Value).Take(2).Select(vote => vote.Key);
            Assert.Equal(new[] { candidate1, candidate2 }, topVotes);
               topCandidates = string.Join(", ", new[] { candidate1, candidate2 });
        }
        


        [Given(@"the (.*) and the (.*) advance the second round")]
        public void WhenTheCandidate1AndTheCandidate3AdvanceTheSecondRound(string candidate1, string candidate2)
        {
            if (_votes == null)
    {
        // Gérer le cas où _votes est null
        throw new Exception("_votes is null");
    }
            var topCandidates = new[] { candidate1, candidate2 };
            _votes = _votes.Where(vote => topCandidates.Contains(vote.Key)).ToDictionary(vote => vote.Key, vote => vote.Value);
        }
        [Then(@"the candidate1 and the candidate2 have equal votes")]
        public void ThenTheCandidate1AndTheCandidate2HaveEqualVotes()
        {  
            var topVotes = _votes.OrderByDescending(vote => vote.Value).Take(2).ToList();
            if (topVotes.Count == 2 && topVotes[0].Value == 0.5 && topVotes[1].Value == 0.5)
            {
                result = "no winner";
                Console.WriteLine(result);
            }

        }
        [Then(@"declare no winner")]
        public void ThenDeclareNoWinner()
        {
           if(
               result=="no winner"
               )
            {
                Console.WriteLine(result);
                    }
        }

    }
}