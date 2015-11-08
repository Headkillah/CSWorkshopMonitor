# WorkshopMonitor

WorkshopMonitor is a custom *Cities: Skylines* mod displaying statistics about the usage of workshop items while in-game. It gathers information about installed workshop items and information about all placed buildings. This includes 'zoned' buildings like residential and commercial buildings, parks, unique buildings and service buildings. 

## Why this mod ##

I'm a big fan of using custom workshop items in my cities. I roam the workshop on a regular basis and keep on subscribing to mods of which I think they might look cool in my current/next city. This has lead to a situation where I've subscribed to hundreds of workshop items without actually using them all. In fact, I think I use maybe half of the workshop items I subscribed to. This has a serious impact on the performance of the game, so recently I wanted to do some cleanup of my workshop items. However, I didn't want to unsubscribe mods that I already used as that might leave gaping holes in my cities, so I needed a way to find out which workshop items I was actually using in my cities and which I wasn't. Hence, the WorkshopMonitor mod. It displays a list of all subscribed workshop items and the number of instances of that workshop item in the current city. 

## Usage
While in-game, hit ctrl+shift+A to open the WorkshopMonitor main dialog. Hit escape or click the close button to hide it again. That's it.

## History

- v0.1: First version     

## Acknowledgements ##
The following mods/modbuilders deserve quite some credit as they made building the WorkshopMonitor mod possible: 

- *Skylines Overwatch* created by [Soda](http://steamcommunity.com/profiles/76561197997507574) ([SteamCommunity](http://steamcommunity.com/sharedfiles/filedetails/?id=421028969)/[GitHub](https://github.com/arislancrescent/CS-SkylinesOverwatch)). This mod is a framework for realtime monitoring of buildings, vehicles, animals and humans. The mod is made to be reused by other mods but I chose to only use parts of the mod in WorkshopMonitor, as I needed some specific tweaks to the building monitoring logic. Still, all the credits for this awesome piece of code goes to Soda.    
- *Extended Public Transport UI* created by [AcidF!re](http://steamcommunity.com/id/AcidF1re) ([SteamCommunity](http://steamcommunity.com/sharedfiles/filedetails/?id=411164732)/[GitHub](https://github.com/justacid/Skylines-ExtendedPublicTransport)). The WorkshopMonitor mod needed some specific UI components to display a large list of items in a grid-like view. The EPTUI mod had these components built-in so these are incorporated as well. The original code has been modified significantly but again, all credits go to AcidF!re, as he showed how to build a nice UI for WorkshopMonitor.
- *ModTools* created by [nlight](http://steamcommunity.com/profiles/76561198027947026)/[BloodyPenguin](http://steamcommunity.com/id/bloody_penguin) ([SteamCommunity](http://steamcommunity.com/sharedfiles/filedetails/?id=450877484)/[GitHub](https://github.com/earalov/Skylines-ModTools)). This is a great mod for modbuilders, giving insight into the inner workings of Cities: Skylines.