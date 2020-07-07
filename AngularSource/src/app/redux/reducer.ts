import { Store } from "./store";
import { Action } from "./action";
import { ActionType } from "./action-type";
import { Movie } from '../models/Movie';
import { LoginUser } from '../models/LoginUser';
import { baseUrl } from 'src/environments/environment';

export class Reducer{
    public static reduce(oldStore: Store, action:Action):Store{
        let newStore:Store = {...oldStore};

        switch(action.type){
            case ActionType.AddUser:
                newStore.loginUser=new LoginUser(action.payload.userNickName, action.payload.userPassword, action.payload.userImdbPass, action.payload.userPicture);
                newStore.isLoggedIn=true;
                break;
            case ActionType.UpdateUser:
                newStore.isLoggedIn=true;
                break;
            case ActionType.UserLogin:
                newStore.loginUser=new LoginUser(action.payload.userNickName, action.payload.userPassword, action.payload.userImdbPass, action.payload.userPicture);
                newStore.isLoggedIn=true;
                break;
            case ActionType.LoginError:
                newStore.loginError=action.payload;
                break;
            case ActionType.SignUpError:
                newStore.signUpError=action.payload;
                break;
            case ActionType.IsLoggedIn:
                newStore.isLoggedIn=action.payload;
                break;
            case ActionType.UserLogOut:
                newStore.loginUser=null;
                newStore.isLoggedIn=false;
                newStore.imdbMovies=[];
                newStore.favoriteMovies=[];
                break;
            
            case ActionType.NeedSignIn:
                newStore.needSignIn=action.payload;
                break;
                
            case ActionType.GetImdbMovies:
                newStore.imdbMovies=action.payload.map(x => new Movie(x.imdbID, x.title, x.plot, x.website, x.rated, x.seen, x.poster, x.userID, x.year));
                break;
            case ActionType.GetImdbMoviesError:
                newStore.imdbMoviesError=action.payload;
                break;
            case ActionType.GetImdbMovie:
                newStore.movie=new Movie(action.payload.imdbID, action.payload.title, action.payload.plot, action.payload.website, action.payload.rated, action.payload.seen, action.payload.poster, action.payload.userID, action.payload.year);
                break;
            case ActionType.GetImdbMovieError:
                newStore.movieError=action.payload;
                break;
            case ActionType.UpdateImdbMovie:
                let updateImdbMovie = new Movie(action.payload.imdbID, action.payload.title, action.payload.plot, action.payload.website, action.payload.rated, action.payload.seen, action.payload.poster, action.payload.userID, action.payload.year);
                let imdbIndex = newStore.imdbMovies.findIndex(item => item.imdbID === updateImdbMovie.imdbID);
                newStore.imdbMovies[imdbIndex] = updateImdbMovie;
                break;
            case ActionType.UpdateImdbMovieError:
                newStore.updateImdbMoviesError=action.payload;
                break;
                
            case ActionType.GetFavoriteMovies:
                newStore.favoriteMovies=action.payload.map(x => new Movie(x.imdbID, x.title, x.plot, x.website, x.rated, x.seen, x.poster, x.userID, x.year));
                break;
            case ActionType.GetFavoriteMoviesError:
                newStore.favoriteMoviesError=action.payload;
                break;
            case ActionType.GetFavoriteMovie:
                newStore.movie=new Movie(action.payload.imdbID, action.payload.title, action.payload.plot, action.payload.website, action.payload.rated, action.payload.seen, action.payload.poster, action.payload.userID, action.payload.year);
                break;
            case ActionType.GetFavoriteMovieError:
                newStore.movieError=action.payload;
                break;
            case ActionType.UpdateFavoriteMovie:
                let updateMovieToFavorite = new Movie(action.payload.imdbID, action.payload.title, action.payload.plot, action.payload.website, action.payload.rated, action.payload.seen, action.payload.poster, action.payload.userID, action.payload.year);
                let FavoriteIndex = newStore.imdbMovies.findIndex(item => item.imdbID === updateMovieToFavorite.imdbID);
                newStore.favoriteMovies[FavoriteIndex] = updateMovieToFavorite;
                break;
            case ActionType.UpdateFavoriteMovieError:
                newStore.updatefavoriteMoviesError=action.payload;
                break;
            case ActionType.DeleteFavoriteMovie:
                newStore.favoriteMovies.forEach( (item, index) => {
                    if(item.imdbID === action.payload)
                        newStore.favoriteMovies.splice(index,1);
                    });
                break;
            case ActionType.DeleteFavoriteMovieError:
                newStore.deletefavoriteMoviesError=action.payload;
                break;
            case ActionType.AddMovieToFavorite:
                let addMovieToFavorite = new Movie(action.payload.imdbID, action.payload.title, action.payload.plot, action.payload.website, action.payload.rated, action.payload.seen, action.payload.poster, action.payload.userID, action.payload.year);
                newStore.favoriteMovies.push(addMovieToFavorite);
                break;
            case ActionType.AddMovieToFavoriteError:
                newStore.addfavoriteMoviesError=action.payload;
                break;
            case ActionType.AddMovieFromFavorite:
                let addMovieFromFavorite = new Movie(action.payload.imdbID, action.payload.title, action.payload.plot, action.payload.website, action.payload.rated, action.payload.seen, action.payload.poster, action.payload.userID, action.payload.year);
                newStore.favoriteMovies.push(addMovieFromFavorite);
                break;
            case ActionType.AddMovieFromFavoriteError:
                newStore.addfavoriteMoviesError=action.payload;
                break;
            case ActionType.SetPlace:
                newStore.place=action.payload;
                break;
            case ActionType.SetCompName:
                newStore.compName=action.payload;
                break;
                
            default:
                break;
        }
        return newStore;
    }
}