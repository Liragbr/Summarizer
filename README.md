# YouTube Video Analyzer

This project, YouTube Video Analyzer, is a comprehensive tool designed to automate the process of downloading, transcribing, and summarizing audio from YouTube videos. It brings together several functionalities into a single, user-friendly application. This README will walk you through each component and explain how to set up, use, and extend the application.


## Overview
The YouTube Video Analyzer is built to facilitate users in analyzing audio content from YouTube videos effortlessly. This tool automates:
1. **Downloading Audio**: Extracts audio from YouTube videos.
2. **Transcribing Audio**: Converts audio content to text.
3. **Generating Summary**: Creates a concise summary of the transcribed text.

## Features
- **Automated Audio Download**: Extracts audio from YouTube using the YoutubeExplode library.
- **Audio Conversion**: Converts downloaded MP3 audio files to WAV format using NAudio.
- **Speech Recognition**: Transcribes audio to text using the built-in System.Speech.Recognition library.
- **Text Summarization**: Generates a summary of the transcription using custom text processing algorithms.

## Technologies Used
- **C#**: Core programming language for developing the application.
- **.NET**: Framework for building and running the application.
- **YoutubeExplode**: Library for downloading YouTube videos and audio streams.
- **NAudio**: Library for audio processing.
- **System.Speech.Recognition**: Library for speech recognition and transcription.

## Installation
To set up the project locally, follow these steps:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/YouTubeVideoAnalyzer.git
2. **Navigate to the project directory**:

    ```bash
    cd YouTubeVideoAnalyzer

3. **Install required libraries**: Ensure you have the necessary NuGet packages installed. You can install them using the NuGet Package Manager:

    ```bash
    YoutubeExplode
    ```

    ```bash
    NAudio
    ```

4. **Build the project**: Open the project in your IDE (e.g., Visual Studio) and build the solution to restore all dependencies.

## Usage
1. **Run the application**: Execute the program from your IDE or command line:
```
dotnet run
```

2. **Input the YouTube video URL**: The program will prompt you to enter the URL of the YouTube video.

3. **Audio Processing**: The application will download the audio, convert it to WAV format, transcribe the content, and generate a summary.

## Project Structure
```
YouTubeVideoAnalyzer/
├── AudioProcessing/
│   ├── AudioDownloader.cs
├── Summarization/
│   └── TextSummarizer.cs
├── Transcription/
│   └── AudioTranscriber.cs
└── Program.cs
```

## Detailed Workflow
### 1. Downloading Audio
The AudioDownloader class handles downloading audio from YouTube videos. It uses the YoutubeClient from YoutubeExplode to fetch the audio stream with the highest bitrate and saves it as an MP3 file.

2. Converting Audio
The downloaded MP3 file is then converted to a WAV file using the ConvertMp3ToWav method. This step is crucial for ensuring compatibility with the transcription process.

3. Transcribing Audio
The AudioTranscriber class leverages the SpeechRecognitionEngine to transcribe the WAV audio file into text. It supports multiple languages, with a fallback to English if the desired language is unavailable.

4. Generating Summary
The TextSummarizer class processes the transcription to generate a concise summary. It ranks sentences based on word frequency and selects the most relevant sentences to form the summary.

## Future Enhancements

- **API Integration**: Integrate with advanced speech recognition APIs like Google Cloud Speech-to-Text or Azure Cognitive Services for more accurate transcriptions.
- **GUI Development**: Develop a graphical user interface to make the tool more user-friendly.
- **Enhanced Summarization**: Implement advanced NLP techniques or leverage pre-trained models like OpenAI's GPT for more sophisticated summarization.
