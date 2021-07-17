import { UserManager } from "oidc-client";
import SharedConstant from "../shared/CenterRepo";

var handleError = (error) => console.log(`Authentication error ${error}`);

class AuthService {
	manager = null;

	constructor() {
		const authConfig = {
			authority: "https://localhost:44380",
			client_id: "spaclient",
			redirect_uri: SharedConstant.base_uri + "/post-login",
			response_type: "code",
			scope: "openid profile product.api",
			post_logout_redirect_uri: SharedConstant.base_uri + "/post-logout",
		};
		this.manager = new UserManager(authConfig);
	}

	getManager() {
		return this.manager;
	}

	login() {
		this.manager.signinRedirect().catch((error) => handleError(error));
	}

	completeLogin() {
		return this.manager
			.signinRedirectCallback()
			.catch((error) => handleError(error));
	}

	logout() {
		this.manager.signoutRedirect().catch((error) => handleError(error));
	}

	completeLogout() {
		this.manager
			.signoutRedirectCallback()
			.then(() => this.manager.removeUser())
			.catch((error) => handleError(error));
	}
}

export default (function () {
	var authServiceInstance;

	return {
		getInstance: function () {
			if (!authServiceInstance) authServiceInstance = new AuthService();
			return authServiceInstance;
		},
	};
})();
