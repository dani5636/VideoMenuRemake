using System;
using System.Collections.Generic;
using VideoMenuBLL;
using VideoMenuEntity;
using static System.Console;
namespace VideoMenuUI
{
    class Program
    {
        static BLLFacade bllFacade = new BLLFacade();
        
        static void Main(string[] args)
        {
            #region Filling Video List

            bllFacade.CustomerService.CreateVideo(new Video()
            {
                Genre = "Horror",
                Name = "The Ring"
            });


            bllFacade.CustomerService.CreateVideo(new Video()
            {
                Genre = "Comedy",
                Name = "Dr. Doolittle"
            });
            bllFacade.CustomerService.CreateVideo(new Video()
            {
                Genre = "Action",
                Name = "Spider Man: Homecoming"
            });
            
            #endregion

            #region Menu Items
            string[] menuItems =
            {
                "Create a video",
                "List all videos",
                "Update a video",
                "Delete a video",
                "Search in all videos",
                "Exit"
            };
            #endregion

            #region Menu Switch
            var selection = ShowMenu(menuItems);
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
                selection = ShowMenu(menuItems);


            }
            #endregion
            WriteLine("Press enter to close the program");
            ReadLine();

        }

        private static void DeleteVideo()
        {
            int id;
            WriteLine("Input the id of the video you would like to delete:(ID)");
            if (int.TryParse(ReadLine(), out id)) {
                var result = bllFacade.CustomerService.DeleteVideo(id)== true ? "Video was succesfully deleted" : "Video was not found";
                WriteLine(result);
            }
            else {
                WriteLine("Please input an id");
            }
        }

        private static void SearchVideos()
        {
            WriteLine("Search for videos");
            var searchedVideos = bllFacade.CustomerService.SearchVideos(ReadLine());
            
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
                if (ConfirmInfo())
                {
                    video.Genre = genre;
                    video.Name = name;
                    bllFacade.CustomerService.UpdateVideo(video);
                    WriteLine("Video has been updated");
                }
                else
                {
                    WriteLine("The video was not updated");
                }
            }
        }

        private static bool ConfirmInfo()
        {
            WriteLine("is this info correct? (Y/N)");
            string accept = ReadLine().ToLower();
            while (!accept.Equals("y") && !accept.Equals("n"))
            {
                WriteLine("Input Y for yes or N for no if the information is right or wrong");
                accept = ReadLine().ToLower();
            }
            if (accept.Equals("y"))
            {
                return true;
            }

            return false;
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
                    return bllFacade.CustomerService.GetVideoById(idSearch);
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

            if (ConfirmInfo())
            {
                bllFacade.CustomerService.CreateVideo(new Video()
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
            foreach (var video in bllFacade.CustomerService.GetAllVideos())
            {
                WriteLine($"ID: {video.Id} |Genre: {video.Genre} |Name: {video.Name}");
            }
        }

        private static int ShowMenu(string[] menuItems)
        {
            Clear();
            WriteLine("Select what you want to do: \n");

            for (int i = 0; i < menuItems.Length; i++)
            {
                WriteLine($"{(i + 1)}: {menuItems[i]}");
            }
            WriteLine("Please select a menu(1-6):");
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection)
                   || selection < 1
                   || selection > 6
            )
            {
                WriteLine("You need to select a number between 1 and 6");
            }

            return selection;
        }
    }
}