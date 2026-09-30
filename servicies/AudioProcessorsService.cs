using FFMpegCore;
using Microsoft.AspNetCore.Components.Forms;
using System;
using VisioForge.Core.MediaBlocks.AudioEncoders;
using VisioForge.Core.MediaBlocks.Sources;
using VisioForge.Core.Types.X.Sources;

namespace ElTasilAcademy.servicies
{
    public class AudioProcessorsService 
    {
        //public void MediaBlocks()
        //{
        //    // Example using a virtual source, but you can replace this with any audio source.
        //    var audioSource = new VirtualAudioSourceBlock(new VirtualAudioSourceSettings());

        //    // Create three AAC encoders for different bitrates
        //    var aacSettings1 = AACEncoderBlock.GetDefaultSettings();
        //    aacSettings1.Bitrate = 128000; // 128 Kbps
        //    var aacEncoder1 = new AACEncoderBlock(aacSettings1);

        //    var aacSettings2 = AACEncoderBlock.GetDefaultSettings();
        //    aacSettings2.Bitrate = 64000; // 64 Kbps
        //    var aacEncoder2 = new AACEncoderBlock(aacSettings2);

        //    var aacSettings3 = AACEncoderBlock.GetDefaultSettings();
        //    aacSettings3.Bitrate = 32000; // 32 Kbps
        //    var aacEncoder3 = new AACEncoderBlock(aacSettings3);
        //}

       

        public async Task ProcessAudioFileAsync(string inputPath)
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            if (IsAudioCompressed(inputPath))
            {
                Console.WriteLine("The audio file is already compressed. No action needed.");
                return;
            }
            else
            {
                Console.WriteLine("Uncompressed audio detected. Compressing now...");

                string outputPath = Path.ChangeExtension(inputPath, ".ogg");

                await CompressAudioAsync(inputPath, outputPath);

                Console.WriteLine($"Compression complete. New file saved as: {outputPath}");
            }
        }

        private static bool IsAudioCompressed(string filePath)
        {
            var mediaInfo = FFProbe.Analyse(filePath);
            var audioStream = mediaInfo.AudioStreams.FirstOrDefault();

            if (audioStream == null)
            {
                // No audio stream found, treat as uncompressed for safety
                return false;
            }

            // The term "uncompressed" typically means PCM, which is often found in WAV files
            // We can check for codecs that are not typically considered lossy-compressed.
            string codecName = audioStream.CodecName.ToLower();
            return codecName != "pcm_s16le" && codecName != "pcm_u8" && !Path.GetExtension(filePath).Equals(".wav", StringComparison.OrdinalIgnoreCase);
        }

        private static async Task CompressAudioAsync(string inputPath, string outputPath)
        {
            await FFMpegArguments
                .FromFileInput(inputPath)
                .OutputToFile(outputPath, false, options => options
                    .WithAudioCodec("libopus")
                    .WithAudioBitrate(64) // Example bitrate, adjust as needed
                    .ForceFormat("ogg"))
                .ProcessAsynchronously();
        }

        public async Task EncodeAudioForBitratesAsync(string inputFilePath, string outputDirectory, int[] bitrates)
        {
            // Ensure FFmpeg is configured. The library can auto-detect it if it's in the PATH.
            // Otherwise, use FFMpegOptions.Configure(options => options.FFMpegBinaryPath = "...");

            var fileInfo = new FileInfo(inputFilePath);
            Directory.CreateDirectory(outputDirectory);

            foreach (var bitrate in bitrates)
            {
                string outputFilePath = Path.Combine(outputDirectory, $"{Path.GetFileNameWithoutExtension(fileInfo.Name)}_{bitrate}kbps.mp3");

                // Build the FFmpeg command for the current bitrate
                await FFMpegArguments
                    .FromFileInput(inputFilePath)
                    .OutputToFile(outputFilePath, overwrite: true, options => options
                        .WithAudioBitrate(bitrate))
                    .ProcessAsynchronously();

                Console.WriteLine($"Encoded {bitrate}kbps version: {outputFilePath}");
            }
        }
    }
}

