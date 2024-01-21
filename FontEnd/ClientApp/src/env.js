(function (window) {
    window.__env = window.__env || {};


      //var baseRoot = baseUrlConfig;
     // var baseRoot = 'https://sins.aztelekom.az:8080';
	  var baseRoot = 'https://dev-sins.aztelekom.az:700';
   // var baseRoot = 'http://localhost:51700/';
    var env = {
        appName: "SINS",
        version: "3.01.001",
        subversion: "2.1",
        apiUrl: baseRoot + '/api/',
        fileUrl: baseRoot + '/file/',
        baseUrl: '/',
        signalRUrl: baseRoot + '/nisHub',
        enableDebug: true,
        releaseDate: "04 mart 2019",
        AD: baseRoot,
        //newFeature: [''],
        changeLog: "http://redmine.aztelekom.az/versions/19",
        userGuideUrl: baseRoot + "/file/userguide.pdf",
        authors: "İsmayıl İsmayılov & Orxan Rzazadə",
        multiTaskId: 45,
        intervals: [],
        timeout: 0
    }
    window.__env = env;
    if (localStorage.subversion && localStorage.subversion != env.subversion) {
        window.localStorage.clear();
        localStorage.subversion = env.subversion
        alert("Proqram versiya yenilənmişdir (v" + env.version + ": c" + env.subversion + "), \nYenidən login olmanızı xahiş edirik")
        location.assign("/pages/auth/login/");
    }
}(this));
