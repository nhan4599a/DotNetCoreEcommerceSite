import { UserManager } from "oidc-client";

var handleError = (error) => console.log(`Authentication error ${error}`);

export default class AuthService {
	manager = null;
	user = null;

	constructor() {
		const authConfig = {
			authority: "https://localhost:44380",
			client_id: "spaclient",
			redirect_uri: "http://localhost:3000",
			response_type: "code",
			scope: "openid profile product.api",
			post_logout_redirect_uri: "http://localhost:3000",
		};
		this.manager = new UserManager(authConfig);
	}

	isLoggedIn() {
		return this.user != null && this.user.access_token && !this.user.expired;
	}

	loadUser() {
		this.manager.getUser().then(user => this.user = user);
	}

	login() {
		this.manager.signinRedirect().catch(error => handleError(error));
	}

	completeLogin() {
		this.manager
			.signinRedirectCallback()
			.then(user => this.user = user)
			.catch(error => handleError(error));
	}

	logout() {
		this.manager.signoutRedirect().catch(error => handleError(error));
	}

	completeLogout() {
		this.manager
			.signoutRedirectCallback()
			.then(() => this.manager.removeUser())
			.then(() => (this.user = null))
			.catch(error => handleError(error));
	}
}
