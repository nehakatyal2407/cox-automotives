import { Deal } from '../interfaces/Deal';
import { UPLOAD_DATA, RESET_DATA, DATA_LOADING_STARTED, DATA_LOADING_ENDED } from './actions';
import { tassign } from 'tassign';

export interface IAppState {
    Deals: Deal[];
    TopDeals: string[];
    isLoading: boolean;
}


export const INITIAL_STATE: IAppState = {
    Deals: [],
    TopDeals: [],
    isLoading: false
}

export function rootReducer(state: IAppState, action): IAppState {
    switch (action.type) {
        case UPLOAD_DATA:
            return tassign(state, {
                Deals: action.body.Deals,
                TopDeals: action.body.TopDeals
            });
        case RESET_DATA:
            return INITIAL_STATE;
        case DATA_LOADING_STARTED:
            return tassign(state, {
                isLoading: true
            });
        case DATA_LOADING_ENDED:
            return tassign(state, {
                isLoading: false
            });


    }
    return state;
}