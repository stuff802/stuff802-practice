var livehn = "districtdesigncode.wakefield.gov.uk"; // live domain
// default to live if this is null or undefined 
var hn = window.location.hostname || livehn;

if (hn == livehn) {
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());
    gtag('consent', 'default', {
        'ad_storage': 'denied',
        'analytics_storage': 'denied',
        'functionality_storage': 'denied',
        'personalization_storage': 'denied',
        'security_storage': 'denied'
    });
    gtag('config', 'G-PFH5FPKQMN');

    var config = {
        "apiKey": "6770c6582f3bd4e8ed21e89e3c4e29b5bad7001e",
        "product": "PRO_MULTISITE",
        necessaryCookies: ['UMB-XSRF-TOKEN', 'UMB-XSRF-V', 'UMB_UCONTEXT', 'UMB_UCONTEXT_C', 'UMB_UPDCHK'],
        optionalCookies: [{
            name: "analytics",
            label: "Google Analytics",
            description: "Analytical cookies help us to improve our website by collecting and reporting information on its usage.",
            cookies: ['_ga', '_ga*', '_gid', '_gat', '__utm*', 'IDE', '_gcl*', 'NID', '___utmvc'],
            onAccept: function () { gtag('consent', 'update', { 'analytics_storage': 'granted' }); },
            onRevoke: function () { gtag('consent', 'update', { 'analytics_storage': 'denied' }); }
        }],
        position: "left",
        initialState: "notify"
    };

    CookieControl.load(config);

    $(function () {
        var $ourCookies = $(`<div>For a full list of cookies we collect please see our <a id="ccc-info-link" class="ccc-link ccc-tabbable" href="/cookies" rel="noopener">Cookies page</a></div><br/>`);

        var cookieInterval = function () {
            var $info = $("div#ccc-info");

            if ($info != null) {
                $info.prepend($ourCookies);
                clearInterval(cookieInterval);
            }
        }
        setInterval(cookieInterval, 1000);
    });
}