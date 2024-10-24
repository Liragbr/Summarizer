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

            // Divide o texto em frases
            var sentences = transcription.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            // Avalia a relevância de cada frase baseado no número de palavras importantes
            var rankedSentences = RankSentencesByRelevance(sentences);

            // Define a quantidade de frases a serem usadas no resumo, por exemplo, 30% do conteúdo
            int numberOfSentencesForSummary = Math.Max(1, sentences.Length / 3);

            // Seleciona as frases mais relevantes para o resumo
            var summarySentences = rankedSentences.Take(numberOfSentencesForSummary).Select(x => x.Sentence).ToList();

            // Junta as frases selecionadas em um texto final
            return string.Join(". ", summarySentences) + ".";
        }

        // Função para pontuar a relevância de cada frase
        private List<(string Sentence, int Score)> RankSentencesByRelevance(string[] sentences)
        {
            var wordFrequency = new Dictionary<string, int>();

            // Conta a frequência de cada palavra (ignorando palavras muito comuns)
            foreach (var sentence in sentences)
            {
                var words = sentence.Split(new[] { ' ', ',', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    var lowerWord = word.ToLower();
                    if (lowerWord.Length > 3) // Ignora palavras muito curtas (ex: "de", "o", "a")
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

            // Pontua cada frase com base na frequência das palavras
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

            // Ordena as frases por pontuação (da mais relevante para a menos relevante)
            return rankedSentences.OrderByDescending(x => x.Score).ToList();
        }
    }
}
