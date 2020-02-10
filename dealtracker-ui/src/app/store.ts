import { Deal } from '../interfaces/Deal';
import { UPLOAD_DATA } from './actions';
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
            const modifiedState = {
                Deals: action.body.deals,
                TopDeals: action.body.topDeals
            };
            return modifiedState;
    }
    return state;
}