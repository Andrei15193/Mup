export const ActionState = {
    excuting: "excuting",
    completed: "completed",
    faulted: "faulted",
    canceled: "cancelled",
    cancelled: "cancelled"
};

export class ActionData {
    constructor(action, state, data) {
        if (!Object.getOwnPropertyNames(ActionState).find(name => (state === ActionState[name])))
            throw new Error("Unknown action state.");

        if (data)
            Object.getOwnPropertyNames(data).reduce(
                (self, propName) => Object.defineProperty(
                    self,
                    propName,
                    {
                        enumerable: true,
                        value: data[propName]
                    }),
                this);

        Object.defineProperty(
            this,
            "action",
            {
                enumerable: true,
                value: action
            });

        Object.defineProperty(
            this,
            "state",
            {
                enumerable: true,
                value: state
            });
    }
}