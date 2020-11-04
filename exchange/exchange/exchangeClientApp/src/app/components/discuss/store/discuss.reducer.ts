import { createReducer, Action, on } from '@ngrx/store';
import { initialDiscussState, IDiscussState } from './discuss.state';
import * as DiscussActions from './discuss.actions';

const reducer = createReducer(initialDiscussState);

export function discussReducer(
  state: IDiscussState | undefined,
  action: Action
) {
  return reducer(state, action);
}
