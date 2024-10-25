using System;
using System.Linq;
using System.Collections.Generic;

namespace YouTubeVideoAnalyzer.Summarization
{
    public class TextSummarizer
    {
        public string GenerateSummary(string transcription)
        {
            if (string.IsNullOrWhiteSpace(transcription))
            {
                return "Transcrição vazia. Nenhum conteúdo para resumir.";
            }

            var sentences = transcription.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            var rankedSentences = RankSentencesByRelevance(sentences);

            int numberOfSentencesForSummary = Math.Max(1, sentences.Length / 3);

            var summarySentences = rankedSentences.Take(numberOfSentencesForSummary).Select(x => x.Sentence).ToList();

            return string.Join(". ", summarySentences) + ".";
        }

        private List<(string Sentence, int Score)> RankSentencesByRelevance(string[] sentences)
        {
            var wordFrequency = new Dictionary<string, int>();

            foreach (var sentence in sentences)
            {
                var words = sentence.Split(new[] { ' ', ',', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    var lowerWord = word.ToLower();
                    if (lowerWord.Length > 3) 
                    {
                        if (wordFrequency.ContainsKey(lowerWord))
                        {
                            wordFrequency[lowerWord]++;
                        }
                        else
                        {
                            wordFrequency[lowerWord] = 1;
                        }
                    }
                }
            }

            var rankedSentences = new List<(string Sentence, int Score)>();
            foreach (var sentence in sentences)
            {
                var score = 0;
                var words = sentence.Split(new[] { ' ', ',', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    var lowerWord = word.ToLower();
                    if (wordFrequency.ContainsKey(lowerWord))
                    {
                        score += wordFrequency[lowerWord];
                    }
                }

                rankedSentences.Add((sentence.Trim(), score));
            }

            return rankedSentences.OrderByDescending(x => x.Score).ToList();
        }
    }
}
