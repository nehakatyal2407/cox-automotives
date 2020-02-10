import { Deal } from '../interfaces/Deal';
import { UPLOAD_DATA, RESET_DATA } from './actions';

import { HttpDealService } from './../services/HttpDealService';

export interface IAppState {
    Deals: Deal[];
    TopDeals: string[];
}


export const INITIAL_STATE: IAppState = {
    Deals: [],
    TopDeals: []
}

export function rootReducer(state: IAppState, action): IAppState {
    switch (action.type) {
        case UPLOAD_DATA:
            return {
                Deals: action.body.deals,
                TopDeals: action.body.topDeals
            };
        case RESET_DATA:
            return INITIAL_STATE;
    }
    return state;
}