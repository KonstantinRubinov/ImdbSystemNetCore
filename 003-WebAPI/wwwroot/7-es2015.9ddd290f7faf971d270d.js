(window.webpackJsonp=window.webpackJsonp||[]).push([[7],{"+ZWi":function(e,t,i){"use strict";i.r(t),i.d(t,"FavoritesModule",(function(){return S}));var o=i("tyNb"),s=i("Q9yQ"),r=i("pLZG"),n=i("fXoL"),a=i("PFzu"),c=i("69fS"),v=i("IfdK"),d=i("iXKo"),l=i("ofXK"),g=i("gGFE");function u(e,t){1&e&&n["\u0275\u0275element"](0,"app-movie-card-vertical",3),2&e&&n["\u0275\u0275property"]("movie",t.$implicit)}function h(e,t){if(1&e&&(n["\u0275\u0275elementStart"](0,"div",1),n["\u0275\u0275template"](1,u,1,1,"app-movie-card-vertical",2),n["\u0275\u0275elementEnd"]()),2&e){const e=n["\u0275\u0275nextContext"]();n["\u0275\u0275advance"](1),n["\u0275\u0275property"]("ngForOf",e.movies)}}const p=[{path:"",component:(()=>{class e{constructor(e,t,i,s,n){this.redux=e,this.favoriteMoviesService=t,this.sessionService=i,this.logger=s,this.movies=[],this.allMoviesAllDataError="",this.place="favorites",this.navigationId=0,this.restoredStateId=0,n.events.pipe(Object(r.a)(e=>e instanceof o.b)).subscribe(e=>{console.group("NavigationStart Event"),this.logger.debug("navigation id: ",e.id),this.navigationId=e.id,this.logger.debug("route: ",e.url),this.logger.debug("trigger: ",e.navigationTrigger),e.restoredState&&(this.restoredStateId=e.restoredState.navigationId,this.logger.warn("restoring navigation id: ",e.restoredState.navigationId)),this.GetSession(e.navigationTrigger),console.groupEnd()})}GetSession(e){let t=[];"popstate"===e?(t=this.sessionService.retrieveSession("favorites"),t&&t.length>0?(this.logger.debug("movies to get: ",t),this.redux.dispatch({type:s.a.GetFavoriteMovies,payload:t}),this.movies=this.redux.getState().favoriteMovies,this.logger.debug("GetSessionFavorites: ",this.movies)):this.favoriteMoviesService.GetAllMovies()):this.favoriteMoviesService.GetAllMovies()}ngOnInit(){this.redux.dispatch({type:s.a.SetPlace,payload:this.place}),this.unsubscribe=this.redux.subscribe(()=>{this.movies=this.redux.getState().favoriteMovies}),this.movies=this.redux.getState().favoriteMovies}ngOnDestroy(){this.movies.length>0&&this.sessionService.storeSession("favorites",this.movies),this.unsubscribe()}}return e.\u0275fac=function(t){return new(t||e)(n["\u0275\u0275directiveInject"](a.NgRedux),n["\u0275\u0275directiveInject"](c.a),n["\u0275\u0275directiveInject"](v.a),n["\u0275\u0275directiveInject"](d.a),n["\u0275\u0275directiveInject"](o.c))},e.\u0275cmp=n["\u0275\u0275defineComponent"]({type:e,selectors:[["app-favorites"]],decls:1,vars:1,consts:[["class","row justify-content-md-center",4,"ngIf"],[1,"row","justify-content-md-center"],["class","col-md-auto center-block col-md-3",3,"movie",4,"ngFor","ngForOf"],[1,"col-md-auto","center-block","col-md-3",3,"movie"]],template:function(e,t){1&e&&n["\u0275\u0275template"](0,h,2,1,"div",0),2&e&&n["\u0275\u0275property"]("ngIf",t.movies&&t.movies.length>0)},directives:[l.i,l.h,g.a],styles:[""]}),e})()}];let f=(()=>{class e{}return e.\u0275mod=n["\u0275\u0275defineNgModule"]({type:e}),e.\u0275inj=n["\u0275\u0275defineInjector"]({factory:function(t){return new(t||e)},imports:[[o.e.forChild(p)],o.e]}),e})();var m=i("Bux+");let S=(()=>{class e{}return e.\u0275mod=n["\u0275\u0275defineNgModule"]({type:e}),e.\u0275inj=n["\u0275\u0275defineInjector"]({factory:function(t){return new(t||e)},imports:[[f,m.a]]}),e})()}}]);