using System;
using System.Collections.Generic;
using VideoMenuBLL;
using VideoMenuEntity;
using static System.Console;
namespace VideoMenuUI
{
    class VideoUI
    {
        static BLLFacade bllFacade = new BLLFacade();
        
        public static void VideoMenu()
        {
           

            #region Menu Items
            string[] menuItems =
            {
                "Create a video",
                "List all videos",
                "Update a video",
                "Delete a video",
                "Search in all videos",
                "Back"
            };
            #endregion

            #region Menu Switch
            var selection = ExtraUI.ShowMenu(menuItems);
            while (selection != 6)
            {
                switch (selection)
                {
                    case 1:
                        CreateVideo();
                        break;
                    case 2:
                        ListAllVideos();
                        break;
                    case 3:
                        UpdateVideo();
                        break;
                    case 4:
                        DeleteVideo();
                        break;
                    case 5:
                        SearchVideos();
                        break;
                }
                WriteLine("Press Enter to go back to the menu");
                ReadLine();
                selection = ExtraUI.ShowMenu(menuItems);


            }
            #endregion
            WriteLine("Press Enter to return to the main menu");
            ReadLine();

        }

        private static void DeleteVideo()
        {
            int id;
            WriteLine("Input the id of the video you would like to delete:(ID)");
            if (int.TryParse(ReadLine(), out id)) {
                var result = bllFacade.VideoService.DeleteVideo(id)== true ? "Video was succesfully deleted" : "Video was not found";
                WriteLine(result);
            }
            else {
                WriteLine("Please input an id");
            }
        }

        private static void SearchVideos()
        {
            WriteLine("Search for videos");
            var searchedVideos = bllFacade.VideoService.SearchVideos(ReadLine());
            
            WriteLine("Search Result");
            foreach (var video in searchedVideos)
            {
                WriteLine($"ID: {video.Id}|Genre: {video.Genre}|Name: {video.Name}");
            }

        }

        

        private static void UpdateVideo()
        {
            WriteLine("Which Video would you like to update? (ID)");
            var video = FindVideoById();

            if (video != null)
            {
                WriteLine("You are updating the following video:");
                WriteLine($"Genre: {video.Genre} |Name: {video.Name}");
                WriteLine("Genre: ");
                var genre = ReadLine();

                WriteLine("Name: ");
                var name = ReadLine();

                WriteLine("You have inputted the following info:");
                WriteLine($"Genre: {genre} |Name: {name}");
                if (ExtraUI.ConfirmInfo())
                {
                    video.Genre = genre;
                    video.Name = name;
                    bllFacade.VideoService.UpdateVideo(video);
                    WriteLine("Video has been updated");
                }
                else
                {
                    WriteLine("The video was not updated");
                }
            }
        }

      

        private static Video FindVideoById()
        {
            WriteLine("Enter Q to go back to the menu");
            Video video = null;
            while (video == null)
            {
                int idSearch;
                string input = ReadLine();

                if (int.TryParse(input, out idSearch))
                {
                    return bllFacade.VideoService.GetVideoById(idSearch);
                }
                else if (input.ToLower().Equals("q"))
                {
                    break;
                }
                else
                {
                    Write("You have to input the id");
                }


            }
            return null;
        }

        private static void CreateVideo()
        {
            WriteLine("Genre: ");
            var genre = ReadLine();

            WriteLine("Name: ");
            var name = ReadLine();

            WriteLine("You have inputted the following info:");
            WriteLine($"Genre: {genre} |Name: {name}");

            if (ExtraUI.ConfirmInfo())
            {
                bllFacade.VideoService.CreateVideo(new Video()
                {
                    Genre = genre,
                    Name = name
                });
                WriteLine("Video is now in information");
            }
            else
            {
                WriteLine("The video was not added");
            }
        }

        private static void ListAllVideos()
        {
            foreach (var video in bllFacade.VideoService.GetAllVideos())
            {
                WriteLine($"ID: {video.Id} |Genre: {video.Genre} |Name: {video.Name}");
            }
        }

       
    }
}