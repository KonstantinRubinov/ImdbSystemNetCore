function _classCallCheck(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function _defineProperties(e,t){for(var n=0;n<t.length;n++){var i=t[n];i.enumerable=i.enumerable||!1,i.configurable=!0,"value"in i&&(i.writable=!0),Object.defineProperty(e,i.key,i)}}function _createClass(e,t,n){return t&&_defineProperties(e.prototype,t),n&&_defineProperties(e,n),e}(window.webpackJsonp=window.webpackJsonp||[]).push([[8],{tg35:function(e,t,n){"use strict";n.r(t),n.d(t,"MovieDetailsModule",(function(){return x}));var i=n("tyNb"),r=n("7kIq"),o=n("fXoL"),a=n("pu2r"),l=n("69fS"),s=n("PFzu"),c=n("ofXK"),d=n("3Pt+");function m(e,t){if(1&e){var n=o["\u0275\u0275getCurrentView"]();o["\u0275\u0275elementStart"](0,"form",4,5),o["\u0275\u0275elementStart"](2,"div",6),o["\u0275\u0275elementStart"](3,"div",7),o["\u0275\u0275elementStart"](4,"h2"),o["\u0275\u0275text"](5),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementStart"](6,"div",6),o["\u0275\u0275elementStart"](7,"div",7),o["\u0275\u0275elementStart"](8,"h5"),o["\u0275\u0275text"](9),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementStart"](10,"div",6),o["\u0275\u0275elementStart"](11,"div",7),o["\u0275\u0275elementStart"](12,"h5"),o["\u0275\u0275text"](13),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementStart"](14,"div",6),o["\u0275\u0275elementStart"](15,"div",7),o["\u0275\u0275elementStart"](16,"h5"),o["\u0275\u0275text"](17),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementStart"](18,"div",6),o["\u0275\u0275elementStart"](19,"div",7),o["\u0275\u0275elementStart"](20,"h5"),o["\u0275\u0275text"](21),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementStart"](22,"button",8),o["\u0275\u0275listener"]("click",(function(){return o["\u0275\u0275restoreView"](n),o["\u0275\u0275nextContext"]().DoCommand()})),o["\u0275\u0275text"](23),o["\u0275\u0275elementEnd"](),o["\u0275\u0275elementEnd"]()}if(2&e){var i=o["\u0275\u0275nextContext"]();o["\u0275\u0275advance"](5),o["\u0275\u0275textInterpolate"](i.movie.title),o["\u0275\u0275advance"](4),o["\u0275\u0275textInterpolate1"]("Year : ",i.movie.year,""),o["\u0275\u0275advance"](4),o["\u0275\u0275textInterpolate1"]("Plot : ",i.movie.plot,""),o["\u0275\u0275advance"](4),o["\u0275\u0275textInterpolate1"]("Rated : ",i.movie.rated,""),o["\u0275\u0275advance"](4),o["\u0275\u0275textInterpolate1"]("Seen : ",i.movie.seen,""),o["\u0275\u0275advance"](2),o["\u0275\u0275textInterpolate"](i.buttonText)}}var v,u,f,p=[{path:"",component:(v=function(){function e(t,n,i,o){_classCallCheck(this,e),this.activatedRoute=t,this.imdbService=n,this.favoriteMoviesService=i,this.redux=o,this.movie=new r.a}return _createClass(e,[{key:"ngOnInit",value:function(){var e=this;this.movie=this.redux.getState().movie,this.place=this.redux.getState().place,this.unsubscribe=this.redux.subscribe((function(){e.movie=e.redux.getState().movie}));var t=this.activatedRoute.snapshot.params.id;"favorites"===this.place?this.favoriteMoviesService.GetMovieById(t.toString()):this.imdbService.GetMovieById(t.toString()),this.buttonText="favorites"===this.place?"Remove from favorites":"Add to favorites"}},{key:"isNumber",value:function(e){return null!=e&&!isNaN(Number(e.toString()))}},{key:"ngOnDestroy",value:function(){this.unsubscribe()}},{key:"DoCommand",value:function(){"favorites"===this.place?this.DeleteFromFavorites():this.AddToFavorites()}},{key:"AddToFavorites",value:function(){this.favoriteMoviesService.AddMovieToFavorite(this.movie)}},{key:"DeleteFromFavorites",value:function(){this.favoriteMoviesService.DeleteFromFavorites(this.movie.imdbID)}}]),e}(),v.\u0275fac=function(e){return new(e||v)(o["\u0275\u0275directiveInject"](i.a),o["\u0275\u0275directiveInject"](a.a),o["\u0275\u0275directiveInject"](l.a),o["\u0275\u0275directiveInject"](s.NgRedux))},v.\u0275cmp=o["\u0275\u0275defineComponent"]({type:v,selectors:[["app-movie-details"]],decls:4,vars:2,consts:[[1,"row"],[1,"col-md-4","col","d-flex","align-items-center","justify-content-center"],["onerror","this.onerror=null;this.src='../../../assets/images/no-image.png';",1,"card-img",3,"src"],["class","col-md-8","id","carData",4,"ngIf"],["id","carData",1,"col-md-8"],["formInfo","ngForm"],[1,"form-row"],[1,"col-md-6"],["type","button",1,"btn","btn-primary",3,"click"]],template:function(e,t){1&e&&(o["\u0275\u0275elementStart"](0,"div",0),o["\u0275\u0275elementStart"](1,"div",1),o["\u0275\u0275element"](2,"img",2),o["\u0275\u0275elementEnd"](),o["\u0275\u0275template"](3,m,24,6,"form",3),o["\u0275\u0275elementEnd"]()),2&e&&(o["\u0275\u0275advance"](2),o["\u0275\u0275propertyInterpolate"]("src",t.movie.poster,o["\u0275\u0275sanitizeUrl"]),o["\u0275\u0275advance"](1),o["\u0275\u0275property"]("ngIf",t.movie))},directives:[c.i,d.m,d.e,d.f],styles:[""]}),v)}],h=((u=function e(){_classCallCheck(this,e)}).\u0275mod=o["\u0275\u0275defineNgModule"]({type:u}),u.\u0275inj=o["\u0275\u0275defineInjector"]({factory:function(e){return new(e||u)},imports:[[i.e.forChild(p)],i.e]}),u),S=n("Bux+"),x=((f=function e(){_classCallCheck(this,e)}).\u0275mod=o["\u0275\u0275defineNgModule"]({type:f}),f.\u0275inj=o["\u0275\u0275defineInjector"]({factory:function(e){return new(e||f)},imports:[[h,S.a]]}),f)}}]);