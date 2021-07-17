import { useEffect } from "react";
import AuthHelper from "../helpers/AuthHelper";

export default function PostLogin() {
	useEffect(() => {
		const authService = AuthHelper.getInstance();
		authService.completeLogin().then(() => (window.location.href = "/"));
	}, []);

	return <></>;
}
