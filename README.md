# Twitch-Spotify-Integration
Integrating a Spotify users currently playing music with the number of viewers livestreaming their Twitch stream

The goal of this project was to solve the DMCA problem Twitch had where their users couldn't play copyrighted music on stream for fear their channel would be struck and taken 
down. My idea was that if we could track the number of average number of users watching a stream during a song we could then have the streamer pay a sum of money based off the 
number of "listeners" to that song. This strategy failed to take into account that users watch VODS after the stream which could count as listens, however that number of listens
is usually very small. 

Honestly, the UI is basic and clunky, but the goal wasn't really to make a finished product; it was to learn more about API's and a bit about password hashing. Most of the 
code/work is found in the UpdateService.cs
