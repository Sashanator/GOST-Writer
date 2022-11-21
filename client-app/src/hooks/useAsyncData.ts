import { useReducer, useEffect, useRef, useState, useCallback } from "react";

export interface State<T = any> {
	loading: boolean;
	data: T;
	error?: any;
	stamp?: number;
}

export type ActionType<T = any> =
	| { type: "request"; stamp: number }
	| { type: "success"; data: T; stamp: number }
	| { type: "error"; error: any; stamp: number };

export function reducer(state: State, action: ActionType) {
	const isEqualStamp = state.stamp && state.stamp === action.stamp;
	switch (action.type) {
		case "request":
			return {
				...state,
				stamp: action.stamp,
				error: undefined,
				loading: true
			};
		case "success":
			if (!isEqualStamp) return state;
			return {
				...state,
				data: action.data,
				loading: false
			};
		case "error":
			if (!isEqualStamp) return state;
			return {
				...state,
				loading: false,
				error: action.error
			};
		default:
			throw Error("Not valid action");
	}
}

interface HandleRefresh {
	handleRefresh: () => void;
}
/**
 * Async data request hook
 * @param method async function to be called
 * @param initialData initial state of data
 * @param deps dependency list. Will trigger method on dependency change
 */
const useAsyncData = <T>(method: () => Promise<T>, initialData: T, deps: any[] = []): State<T> & HandleRefresh => {
	const initialState = { loading: true, data: initialData };
	const unmount = useRef(false); //if useEffect run cancelation function, we set flat to true
	const [refresh, setRefresh] = useState(new Date().getTime());
	const handleRefresh = useCallback(() => { setRefresh(new Date().getTime()) }, []);

	const [state, dispatch] = useReducer(reducer, initialState);

	useEffect(() => {
		//unmount feature
		return () => {
			//unmount.current = true;
		};
	}, []);

	useEffect(() => {
		(async () => {
			const stamp = new Date().getTime();
			try {
				dispatch({ type: "request", stamp });
				const data = await method();
				if (!unmount.current) dispatch({ type: "success", data, stamp });
			} catch (error) {
				if (!unmount.current) dispatch({ type: "error", error, stamp });
			}
		})();
	}, [...deps, refresh]);

	return { ...state, handleRefresh };
};

export { useAsyncData };
