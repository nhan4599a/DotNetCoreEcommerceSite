import { useEffect } from "react";
import AuthHelper from "../helpers/AuthHelper";

export default function Login() {
	useEffect(() => {
		const authService = AuthHelper.getInstance();
		authService.login();
	}, []);

	return <></>;
}
