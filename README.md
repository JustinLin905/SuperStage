# SuperStage
Supercharge your presentations with another dimension. [*1st Place Winner at HackWestern 10!*](https://devpost.com/software/superstage)

## Inspiration 💥
Let's be honest... Presentations can be super boring to watch—_and_ to present.
But, what if you could bring your biggest ideas to life in a VR world that literally puts you _in_ the PowerPoint? Step beyond slides and into the future with SuperStage!

## What it does 🌟

SuperStage works in 3 simple steps:
1. Export any slideshow from PowerPoint, Google Slides, etc. as a series of images and import them into SuperStage.
2. Join your work/team/school meeting from your everyday video conferencing software (Zoom, Google Meet, etc.).
3. Instead of screen-sharing your PowerPoint window, screen-share your SuperStage window!

And just like that, your audience can watch your presentation as if you were Tim Cook in an Apple Keynote. You see a VR environment that feels exactly like standing up and presenting in real life, and the audience sees a 2-dimensional, front-row seat video of you on stage. It’s simple and only requires the presenter to own a VR headset.

Intuition was our goal when designing SuperStage: instead of using a physical laser pointer and remote, we used full-hand tracking to allow you to be the wizard that you are, pointing out content and flicking through your slides like magic. You can even use your hands to trigger special events to spice up your presentation! Make a fist with one hand to switch between 3D and 2D presenting modes, and make two thumbs-up to summon an epic fireworks display. Welcome to the next dimension of presentations!

## How we built it 🛠️

SuperStage was built using Unity 2022.3 and the C# programming language. A Meta Quest 2 headset was the hardware portion of the hack—we used the 4 external cameras on the front to capture hand movements and poses. We built our UI/UX using ray interactables in Unity to be able to flick through slides from a distance.

## Challenges we ran into 🌀
- 2-camera system. SuperStage is unique since we have to present 2 different views—one for the presenter and one for the audience. Some objects and UI in our scene must be occluded from view depending on the camera.
- Dynamic, automatic camera movement, which locked onto the player when not standing in front of a slide and balanced both slide + player when they were in front of a slide.

To build these features, we used multiple rendering layers in Unity where we could hide objects from one camera and make them visible to the other. We also wrote scripting to smoothly interpolate the camera between points and track the Quest position at all times.

## Accomplishments that we're proud of 🎊
- We’re super proud of our hand pose detection and gestures: it really feels so cool to “pull” the camera in with your hands to fullscreen your slides.
- We’re also proud of how SuperStage uses the extra dimension of VR to let you do things that aren’t possible on a laptop: showing and manipulating 3D models with your hands, and immersing the audience in a different 3D environment depending on the slide. These things add so much to the watching experience and we hope you find them cool!

## What we learned 🧠

Justin: I found learning about hand pose detection so interesting. Reading documentation and even anatomy diagrams about terms like finger abduction, opposition, etc. was like doing a science fair project.

Lily: The camera system! Learning how to run two non-conflicting cameras at the same time was super cool. The moment that we first made the switch from 3D -> 2D using a hand gesture was insane to see actually working.

Carolyn: I had a fun time learning to make cool 3D visuals!! I learned so much from building the background environment and figuring out how to create an awesome firework animation—especially because this was my first time working with Unity and C#! I also grew an even deeper appreciation for the power of caffeine… but let’s not talk about that part :)


## What's next for SuperStage ➡️
- Dynamically generating presentation boards to spawn as the presenter paces the room
- Providing customizable avatars to add a more personal touch to SuperStage
- Adding a lip-sync feature that takes volume metrics from the Oculus headset to generate mouth animations

