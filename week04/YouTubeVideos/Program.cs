class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("10 Tips for Better Sleep", "HealthyHabits", 540);
        video1.AddComment(new Comment("Alice", "This helped me so much, thank you!"));
        video1.AddComment(new Comment("Bob", "I tried tip #3 and it worked amazingly."));
        video1.AddComment(new Comment("Carlos", "Great content as always!"));
        video1.AddComment(new Comment("Diana", "Sharing this with my whole family."));
        videos.Add(video1);

        Video video2 = new Video("Beginner's Guide to Python", "CodeWithMike", 1200);
        video2.AddComment(new Comment("Emma", "Best Python tutorial I've seen."));
        video2.AddComment(new Comment("Frank", "Clear and easy to follow, thanks!"));
        video2.AddComment(new Comment("Grace", "Can you make one for JavaScript too?"));
        videos.Add(video2);

        Video video3 = new Video("How to Make Homemade Pasta", "ItalianKitchen", 780);
        video3.AddComment(new Comment("Henry", "Made this last night, absolutely delicious!"));
        video3.AddComment(new Comment("Isabella", "Finally a recipe that actually works."));
        video3.AddComment(new Comment("James", "Love your channel, keep it up!"));
        video3.AddComment(new Comment("Karen", "What brand of flour do you recommend?"));
        videos.Add(video3);

        Video video4 = new Video("Morning Yoga for Beginners", "ZenFlow", 900);
        video4.AddComment(new Comment("Liam", "Did this routine for 30 days straight!"));
        video4.AddComment(new Comment("Mia", "So relaxing, exactly what I needed."));
        video4.AddComment(new Comment("Noah", "Great pace for beginners."));
        videos.Add(video4);

        foreach (Video video in videos)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine($"Title:    {video.GetTitle()}");
            Console.WriteLine($"Author:   {video.GetAuthor()}");
            Console.WriteLine($"Length:   {video.GetLength()} seconds");
            Console.WriteLine($"Comments: {video.GetNumberOfComments()}");
            Console.WriteLine();

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  {comment.GetCommenterName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }
    }
}