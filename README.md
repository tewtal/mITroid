mITroid Music Converter
=======================

This tool will convert .it modules into a data format usable with Nintendos N-SPC music engine.
In this case specifically for Super Metroid and Legend of Zelda: A Link to the Past.

It can also be used to create stand-alone SPC-files by chosing to discard all the built-in sound effect samples and other data that would normally be required to keep for inclusion in a game.

Please note that this tool is very much still in beta and may have features be changed or broken at any time.

Output
======

The datafile output is in the same format as the games own music data, so it can be included in a ROM like this:
Note that this example is for Super Metroid and will not work for Link to the Past where some slightly different code is needed.

```
lorom

org $8FE817 ;replace intro music in super metroid
DL newmusic

org $B88000
newmusic:
incbin musicdata.nspc
```

Features
========

Only a subset of IT commands and features are supported right now and for some of them the effect
you hear in the tracker will not accurately match the output in the SPC data.
You'll have to generate an SPC in many cases and listen to that to know the final result.

The IT module needs to have exactly 8 channels.

There's a limitation when it comes to patterns. Notes will not sustain over pattern changes.
This can be disabled by activating the N-SPC patch "Disable key-off between patterns"

Reusing patterns as much as possible in multiple places is a good way to save space in the final output. The converter
will also try to de-duplicate any single channels in a pattern that have the exact same notes and effects as a channel in
another pattern.

Normally the song will restart from the first pattern in the "Sequence" defined in the IT file.
You can change the looping point by entering the sequence number (and only this number) in the comment part of the IT file, numbers start from 0.

When converting a file you'll get a status report on how much space was used for instruments/samples/data. If anything exceeds 100% then you'll have to
work on reducing the size of that section or the file won't be playable.

Each sample in the file needs to also have an instrument assigned to it.

There is a new concept of "virtual" samples where you can have samples in the tracker that points to a single sample on the SNES.
This can be useful for example if you want to transpose a single sample to different pitches, but not have the duplicate samples take up space in the song.
To make a sample virtual, enter "<XX" or ">YY" in the "File" field for the sample, where XX is the sample number in the tracker or YY is a direct sample number in the SPC.
The ">YY" variant can be useful if you want to use a built-in sample for example.

Supported IT effects
=====================

The following standard effects are supported:

| Command | Effect          | Notes                                                                                                                                                                                                                                                           |
|---------|-----------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Hxy		  | Vibrato		      |Vibrato with X rate and Y depth (differs slightly from tracker and final SPC output) |
| Rxy		  | Tremolo		      |Tremolo with X rate and Y depth (differs slightly from tracker and final SPC output) |
| Mxx		  | Volume		      |Sets the channel volume to XX (use this to set a new global volume for the channel) |
| Axx		  | Speed		        |Sets a new speed in "ticks per note" |
| Txx		  | Tempo		        |Sets a new song tempo (using this one is preferred over the speed command) |
| Xxx		  | Pan             |Pans the channel to left or right depending on XX |
| Dxx		  | Fade		        |Volume fade up or down for the current row (00->0f = fade down, 00-f0 = fade up) 0-f is the amount to fade amount per tick. |
| DFx/DxF	| Fine fade	      |Fine volume fade, x=0-f and is the volume amount per row to change |
| Gxx		  | Pitchbend	      |Pitch bends towards the note the effect is associated with with XX speed |
| Fxx		  | Portamento up   |	Pitch bends XX/16 semitones up for the current row |
| Exx		  | Portamento down |	Pitch bends XX/16 semitones down for the current row |
| Kxy		  | Fade+Vibrato 	  |Equivalent to volume slide (Dxy) plus Vibrato (H00). The xy parameter affects the volume slide thus works like the parameters of the Dxy command. The vibrato effect uses the last specified vibrato parameters from a Hxy or Uxy command on this channel. |
| Lxy		  | Fade+Pitchbend  |Equivalent to volume slide (Dxy) plus Pitchbend   (G00). The xy parameter affects the volume slide thus works like the parameters of the Dxy command. The tone portamento effect uses the last specified portamento speed from a Gxx command. |
| SDx		  | Note delay	    |Delays the note or instrument change in the same pattern cell by x ticks. If x is greater than or equal the current speed, the content of this cell is never played. SD0 behaves the same as SD1. |


Volume column effects:

| Command | Effect          | Notes                                                                                                                                                                                                                                                           |
|---------|-----------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| axx		  | Fine Fade Up	  | Just like DxF, this slides the volume up x units on the first tick. This command shares memory with all other volume slides command in the volume column in compatible mode, and also with any other slide commands in the volume column and Dxy otherwise.
| bxx		  | Fine Fade Down  |	Just like DFx, this slides the volume down x units on the first tick. The same memory rules as with axx apply.
| cxx		  | Fade Up		      | Just like Dx0, this slides the volume up x units on all ticks but the first. The same memory rules as with axx apply.
| dxx		  | Fade Down	      | Just like D0x, this slides the volume down x units on all ticks but the first. The same memory rules as with axx apply.
| exx		  | Portamento Down |	Just like Exx, this lowers the note frequency. Parameters are four times less precise than those of Exx, so for example E04 equals e01.
| fxx		  | Portamento Up   |	Same as exx, but increases the note frequency.
| gxx		  | Pitchbend       |	Just like Gxx, this pitch-bends from the previous note to the current note. Parameters 1 through 9 translate to the following Gxx commands: G01, G04, G08, G10, G20, G40, G60, G80, GFF.		
| hxx		  | Vibrato		      | Sets the vibrato depth to x and executes a vibrato (like the Hxy command).
| pxx		  | Pan		          | Set the panning to x, where x ranges from 0 to 64 (decimal).
| vxx		  | Volume		      | This is the volume on the volume column, it sets the volume for the current note and resets when the next note starts.


The following special commands exist to control SPC-only things (this can't be heard in the tracker at all)
For each of these special commands, another set of Zxx commands below it are required to provide parameters.

| Command | Effect          | Parameters | Notes                                                                                                                                                                                                                                                           |
|---------|-----------------|------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| S05		  | EON/VOL		      | 3		       | Initates settings for echo on flags and volume.
|				  |                 | Zff		     | Sets the EON flag (which channels have echo enabled, each bit is a channel)
|				  |                 | Zll		     | Sets echo volume on left channel
|				  |                 | Zrr		     | Sets echo volume on right channel and executes the full S05 echo command.
| S06		  | EOFF            | 0		       | Echo off
| S07		  | EDL/EFB/EFL     |	3		       | Echo delay, echo feedback, echo filter
|				  |                 | Zdd		     | Sets echo delay (1-2)
|			 	  |                 | Zff		     | Sets echo feedback (00-7f)
|				  |                 | Zll		     | Sets echo FIR filter (0-3 seems to work here). Setting this also executes the full S07 command.
| S08		  | EFADE           |	3		       | Fades echo volume to a specific value over time
|	 	      |                 |	Ztt		     | Fade time in ticks
|				  |                 | Zll		     | Left channel target volume
|				  |                 | Zrr		     | Right channel target volume. Setting this also executes the full S08 command.

Example:
S05
ZFF
Z7F
Z7F

Setting these in a row like this runs the S05 command and sets echo on for all channels with echo volume 7f on both left and right channels.

This project is using code from the following sources
=====================================================

BRRtools
========

by Bregalad. Special thanks to Kode54.
Minor post-3.11 fixes by Optiroc (David Lindecrantz).
BRRtools are currently the most evolved tools to convert between standard RIFF .wav format and SNES's built-in BRR sound format. 
They have many features never seen before in any other converter, and are open source.
Versions up to 2.1 used to be coded in Java, requiring a Java virtual machine to run. 
Because this was an useless layer of abstraction which is only useful when developing, the program was rewritten to not need Java any longer.
I heavily borrowed encoding algorithms from Kode54, which himself heavily borrowed code from some other ADPCM encoder. 
This is freeware, feel free to redistribute/improve but DON'T CLAIM IT IS YOUR OWN WORK THANK YOU.
