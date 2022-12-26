using GestionCinema.Data;

namespace GestionCinema.Models;

public class SeedData
{
    public static void Init()
    {
        using (var context = new CinemaContext())
        {
            // Test s'il existe déjà des cinémas dans la bdd
            if (!context.Cinemas.Any() && !context.Films.Any() && !context.Salles.Any() && !context.Seances.Any())
            {
                // Ajout de plusieurs cinémas
                Cinema levallois = new Cinema
                {
                    Nom = "Rivay Levallois",
                    Adresse = "28 rue d'Alsace",
                    CodePostal = 92300,
                    Ville = "Levallois",
                    Responsable = "Lucie Balkany",
                    PrixPlace = 7.5
                };
                Cinema amiens = new Cinema
                {
                    Nom = "Amiens",
                    Adresse = "3 boulevard de Belfort",
                    CodePostal = 80000,
                    Ville = "Amiens",
                    Responsable = "Simone Beauchamp",
                    PrixPlace = 7.5
                };
                Cinema europe = new Cinema
                {
                    Nom = "Cité Europe",
                    Adresse = "10 boulevard du Kent",
                    CodePostal = 62230,
                    Ville = "Coquelles",
                    Responsable = "Olivier Brousse",
                    PrixPlace = 7.5
                };
                Cinema madeleine = new Cinema
                {
                    Nom = "Madeleine",
                    Adresse = "36 avenue du Maréchal Foch",
                    CodePostal = 13000,
                    Ville = "Marseille",
                    Responsable = "Lorraine Crête",
                    PrixPlace = 7.5
                };
                Cinema joliette = new Cinema
                {
                    Nom = "La joliette",
                    Adresse = "54 rue de Chanterac",
                    CodePostal = 13000,
                    Ville = "Marseille",
                    Responsable = "David Arnoux",
                    PrixPlace = 7.5
                };
                Cinema alesia = new Cinema
                {
                    Nom = "Alésia",
                    Adresse = "73 avenue du Général Leclerc",
                    CodePostal = 75014,
                    Ville = "Paris",
                    Responsable = "Remy Robert",
                    PrixPlace = 7.5
                };
                Cinema aquaboulevard = new Cinema
                {
                    Nom = "Aquaboulevard",
                    Adresse = "16 rue du Colonel Pierre Avia",
                    CodePostal = 75015,
                    Ville = "Paris",
                    Responsable = "Pascaline Martel",
                    PrixPlace = 7.5
                };
                Cinema elysee = new Cinema
                {
                    Nom = "Champs-Elysées",
                    Adresse = "32 Avenue des Champs-Elysées",
                    CodePostal = 75008,
                    Ville = "Paris",
                    Responsable = "Thibaut Fontaine",
                    PrixPlace = 7.5
                };
                Cinema opera = new Cinema
                {
                    Nom = "Opéra",
                    Adresse = "32 rue Louis le Grand",
                    CodePostal = 75009,
                    Ville = "Paris",
                    Responsable = "Frédérique Lanctot",
                    PrixPlace = 7.5
                };
                Cinema wilson = new Cinema
                {
                    Nom = "Wilson",
                    Adresse = "3 place du Président Thomas",
                    CodePostal = 31000,
                    Ville = "Toulouse",
                    Responsable = "Marjolaine Chensay",
                    PrixPlace = 7.5
                };
                Cinema comedie = new Cinema
                {
                    Nom = "Comédie",
                    Adresse = "10 place de la Comédie",
                    CodePostal = 34000,
                    Ville = "Montpellier",
                    Responsable = "Serge Charlebois",
                    PrixPlace = 7.5
                };
                context.Cinemas.AddRange(levallois, amiens, europe, madeleine, joliette, alesia, aquaboulevard, elysee, opera, wilson, comedie);

                // Ajout de plusieurs films
                Film pulpFiction = new Film
                {
                    Nom = "Pulp Fiction",
                    Realisateur = "Quentin Tarantino",
                    Resume = "L'odyssée sanglante et burlesque de petits malfrats dans la jungle de Hollywood à travers trois histoires qui s'entremêlent.",
                    Genre = Genre.Drame,
                    Date = DateTime.Parse("1994-10-26"),
                    Duree = 145,
                };
                Film inglourious = new Film
                {
                    Nom = "Inglourious Basterds",
                    Realisateur = "Quentin Tarantino",
                    Resume = "Dans la France occupée de 1940, Shosanna Dreyfus assiste à l'exécution de sa famille tombée entre les mains du colonel nazi Hans Landa.",
                    Genre = Genre.Guerre,
                    Date = DateTime.Parse("2009-08-19"),
                    Duree = 153,
                };
                Film mome = new Film
                {
                    Nom = "La Môme",
                    Realisateur = "Olivier Dahan",
                    Resume = "De son enfance à la gloire, de ses victoires à ses blessures, de Belleville à New York, l'exceptionnel parcours d'Edith Piaf. A travers un destin plus incroyable qu'un roman, découvrez l'âme d'une artiste et le coeur d'une femme. Intime, intense, fragile et indestructible, dévouée à son art jusqu'au sacrifice, voici la plus immortelle des chanteuses...",
                    Genre = Genre.Drame,
                    Date = DateTime.Parse("2007-02-14"),
                    Duree = 140,
                };
                Film theArtist = new Film
                {
                    Nom = "The Artist",
                    Realisateur = "Michel Hazanavicius",
                    Resume = "Hollywood 1927. George Valentin est une vedette du cinéma muet à qui tout sourit. L'arrivée des films parlants va le faire sombrer dans l'oubli. Peppy Miller, jeune figurante, va elle, être propulsée au firmament des stars. Ce film raconte l'histoire de leurs destins croisés, ou comment la célébrité, l'orgueil et l'argent peuvent être autant d'obstacles à leur histoire d'amour.",
                    Genre = Genre.Comédie,
                    Date = DateTime.Parse("2012-01-25"),
                    Duree = 160,
                };
                Film planeteSinge = new Film
                {
                    Nom = "La Planète des singes",
                    Realisateur = "Rupert Wyatt",
                    Resume = "Dans un laboratoire, des scientifiques expérimentent un traitement sur des singes pour vaincre la maladie d’Alzheimer. Mais leurs essais ont des effets secondaires inattendus : ils découvrent que la substance utilisée permet d’augmenter radicalement l’activité cérébrale de leurs sujets. César, est alors le premier jeune chimpanzé faisant preuve d’une intelligence remarquable. Mais trahi par les humains qui l’entourent et en qui il avait confiance, il va mener le soulèvement de toute son espèce contre l’Homme dans un combat spectaculaire.",
                    Genre = Genre.Action,
                    Date = DateTime.Parse("2011-08-05"),
                    Duree = 170,
                };
                Film terminator = new Film
                {
                    Nom = "Terminator",
                    Realisateur = "James Cameron",
                    Resume = "Un Terminator, robot d'aspect humain, est envoyé d'un futur où sa race livre aux hommes une guerre sans merci. Sa mission est de trouver et d'éliminer Sarah Connor avant qu'elle ne donne naissance à John, appelé à devenir le chef de la résistance. Cette dernière envoie un de ses membres, Reese, aux trousses du cyborg.",
                    Genre = Genre.ScienceFiction,
                    Date = DateTime.Parse("1985-04-24"),
                    Duree = 167,
                };
                context.Films.AddRange(pulpFiction, inglourious, mome, theArtist, planeteSinge, terminator);

                //Ajout de plusieurs salles 
                Salle levallois1 = new Salle
                {
                    Cinema = levallois,
                    NbPlace = 250,
                    NumeroSalle = 6,
                };
                Salle levallois2 = new Salle
                {
                    Cinema = levallois,
                    NbPlace = 400,
                    NumeroSalle = 12,
                };
                Salle comedie1 = new Salle
                {
                    Cinema = comedie,
                    NbPlace = 458,
                    NumeroSalle = 1,
                };
                Salle comedie2 = new Salle
                {
                    Cinema = comedie,
                    NbPlace = 754,
                    NumeroSalle = 2,
                };
                Salle comedie3 = new Salle
                {
                    Cinema = comedie,
                    NbPlace = 550,
                    NumeroSalle = 3,
                };
                Salle opera1 = new Salle
                {
                    Cinema = opera,
                    NbPlace = 150,
                    NumeroSalle = 1,
                };
                Salle opera2 = new Salle
                {
                    Cinema = opera,
                    NbPlace = 270,
                    NumeroSalle = 2,
                };
                Salle opera3 = new Salle
                {
                    Cinema = opera,
                    NbPlace = 320,
                    NumeroSalle = 3,
                };
                Salle wilson2 = new Salle
                {
                    Cinema = wilson,
                    NbPlace = 350,
                    NumeroSalle = 2,
                };
                Salle wilson4 = new Salle
                {
                    Cinema = wilson,
                    NbPlace = 260,
                    NumeroSalle = 4,
                };
                Salle wilson6 = new Salle
                {
                    Cinema = wilson,
                    NbPlace = 200,
                    NumeroSalle = 6,
                };
                Salle joliette1 = new Salle
                {
                    Cinema = joliette,
                    NbPlace = 150,
                    NumeroSalle = 1,
                };
                context.Salles.AddRange(levallois1, levallois2, comedie1, comedie2, comedie3, opera1, opera2, opera3, wilson2, wilson4, wilson6, joliette1);

                //Ajout de plusieurs séances
                context.Seances.AddRange(new Seance
                {
                    Film = pulpFiction,
                    Salle = levallois1,
                    Date = DateTime.Parse("2023-06-12"),
                    NbPlaceAchete = 142,
                },
                new Seance
                {
                    Film = theArtist,
                    Salle = levallois1,
                    Date = DateTime.Parse("2023-02-25"),
                    NbPlaceAchete = 70,
                },
                new Seance
                {
                    Film = inglourious,
                    Salle = levallois2,
                    Date = DateTime.Parse("2023-01-28"),
                    NbPlaceAchete = 320,
                },
                new Seance
                {
                    Film = inglourious,
                    Salle = joliette1,
                    Date = DateTime.Parse("2023-01-15"),
                    NbPlaceAchete = 80,
                },
                new Seance
                {
                    Film = inglourious,
                    Salle = joliette1,
                    Date = DateTime.Parse("2023-02-15"),
                    NbPlaceAchete = 23,
                },
                new Seance
                {
                    Film = planeteSinge,
                    Salle = opera1,
                    Date = DateTime.Parse("2023-01-10"),
                    NbPlaceAchete = 70,
                },
                new Seance
                {
                    Film = terminator,
                    Salle = opera2,
                    Date = DateTime.Parse("2023-01-08"),
                    NbPlaceAchete = 110,
                },
                new Seance
                {
                    Film = mome,
                    Salle = opera2,
                    Date = DateTime.Parse("2023-01-08"),
                    NbPlaceAchete = 24,
                }
                );

                // Commit changes into DB
                context.SaveChanges();
            }
        }
    }
}