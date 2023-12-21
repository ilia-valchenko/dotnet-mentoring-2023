var createSession = function () {
    return "DummySessionValueMakeItABitLongerfjnvpjnvpinvpiwnvpiwenvpiwjenvpwenvpwvnwvnwpjenvpiernvpienrvpiwnerpvwnepvnwpivjnpwierjvnpweivn";
};

var createNonce = function () {
    return "DummyNonceValuelkewfmqkfkqemfqekrmfqoekwmnfoeqwnfpqejrnfpjqernfpqewnfpqejnpfqejrnpqenfpqnefp";
};

// Before we make an authorization request we need to authenticate with an IdentityServer.
// We have to craft the value for the Return URL and once we log in the Return URL
// is gonna send it to Authorization endpoint where the callback parameter is set
// to whatever we need to return.

var signIn = function () {
    var redirectUri = "https://localhost:7268/Home/SignIn";
    var responseType = "id_token token";
    var scope = "openid catalog-api";

    // We are encoding a navigation to the Authorization endpoint.
    var authUrl =
"/connect/authorize/callback" +
"?client_id=client_id_js" +
"&redirect_uri=" + encodeURIComponent(redirectUri) +
"&response_type=" + encodeURIComponent(responseType) +
"&scope=" + encodeURIComponent(scope) +
"&nonce=" + createNonce() +
"&session=" + createSession();

    // FYI: response_mode is optional.
    // It something what belongs to the Authorization Code Flow
    // rather than Implicit Flow.

    var returnUrl = encodeURIComponent(authUrl);

    console.log(authUrl);
    console.log('// ----------------------- //');
    console.log(returnUrl);

    var identityServerBaseAddress = "https://localhost:7193";

    window.location.href = `${identityServerBaseAddress}/Auth/Login?ReturnUrl=${returnUrl}`;
};