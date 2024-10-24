using System;
using System.IO;
using NAudio.Wave;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace YouTubeVideoAnalyzer.AudioProcessing
{
    public class AudioDownloader
    {
        private readonly YoutubeClient _youtubeClient = new YoutubeClient();

        private string SaveFileToDocuments(string fileName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(documentsPath, fileName);
        }

        public async void DownloadAudioAsync(string videoUrl)
        {
            try
            {
                var streamManifest = await _youtubeClient.Videos.Streams.GetManifestAsync(videoUrl);
                var audioStreamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                string mp3FilePath = SaveFileToDocuments("audio.mp3");
                await _youtubeClient.Videos.Streams.DownloadAsync(audioStreamInfo, mp3FilePath);
                Console.WriteLine("Download concluído: audio.mp3");

                // Converte o MP3 para WAV
                string wavFilePath = SaveFileToDocuments("audio.wav");
                ConvertMp3ToWav(mp3FilePath, wavFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao baixar o áudio: {ex.Message}");
            }
        }

        public void ConvertMp3ToWav(string mp3File, string outputWavFile) // Alterado para public
        {
            try
            {
                using (var reader = new MediaFoundationReader(mp3File)) // Alterado para MediaFoundationReader
                {
                    var outFormat = new WaveFormat(16000, 1); // 16 kHz, mono
                    using (var resampler = new MediaFoundationResampler(reader, outFormat))
                    {
                        resampler.ResamplerQuality = 60;
                        WaveFileWriter.CreateWaveFile(outputWavFile, resampler);
                    }
                }
                Console.WriteLine("Conversão de MP3 para WAV concluída.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante a conversão de MP3 para WAV: {ex.Message}");
            }
        }
    }
}
