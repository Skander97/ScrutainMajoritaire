using System;
using System.Collections.Generic;
using System.Linq;

namespace ScrutainMajoritaire
{

    public class VoteResult
    {
        // Type du résultat (gagnant unique, second tour, aucun gagnant)
        public VoteResultType ResultType { get; set; }
        // Liste des gagnants (peut contenir plusieurs éléments en cas d'égalité)
        public List<string> Winners { get; set; }
    }

    // Enum qui représente les différents types de résultat de vote
    public enum VoteResultType
    {
        SingleWinner,
        SecondRound,
        NoWinner
    }

    public static class VoteCalculator
    {
        // Calcule le résultat d'un vote en fonction des pourcentages de chaque candidat
        public static VoteResult CalculateVoteResult(Dictionary<string, double> votes)
        {
            var totalVotes = votes.Sum(v => v.Value);
            var topCandidates = votes.OrderByDescending(v => v.Value).Take(2).ToList();

            // Si le candidat en tête a plus de 50% des votes, il est déclaré gagnant
            if (topCandidates[0].Value / totalVotes > 0.5)
            {
                return new VoteResult
                {
                    ResultType = VoteResultType.SingleWinner,
                    Winners = new List<string> { topCandidates[0].Key }
                };
            }
            // Si les deux premiers candidats ont des pourcentages proches, on organise un second tour
            else if (topCandidates[0].Value / totalVotes - topCandidates[1].Value / totalVotes < 0.1)
            {
                return new VoteResult
                {
                    ResultType = VoteResultType.SecondRound,
                    Winners = topCandidates.Select(v => v.Key).ToList()
                };
            }
            // Sinon, il n'y a pas de gagnant
            else
            {
                return new VoteResult
                {
                    ResultType = VoteResultType.NoWinner,
                    Winners = new List<string>()
                };
            }
        }
    }
}