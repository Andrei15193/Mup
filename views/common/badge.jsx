import React from "react";

import Style from "mup/style";

export const BadgeType = {
    "Primary": "primary",
    "Secondary": "secondary",
    "Success": "success",
    "Danger": "danger",
    "Warning": "warning",
    "Info": "info",
    "Light": "light",
    "Dark": "dark"
};

export class Badge extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <span className={getClassNames.call(this).join(" ")}>
                {this.props.children}
            </span>
        );
    }
};

function getClassNames() {
    const classNames = [Style.badge];

    const specializedBadgeClassName = getSpecializedBadgeClassName(this.props.type);
    if (specializedBadgeClassName)
        classNames.push(specializedBadgeClassName);

    return classNames;
}

function getSpecializedBadgeClassName(badgeType) {
    switch (badgeType) {
        case BadgeType.Primary:
            return Style.badgePrimary;

        case BadgeType.Secondary:
            return Style.badgeSecondary;

        case BadgeType.Success:
            return Style.badgeSuccess;

        case BadgeType.Danger:
            return Style.badgeDanger;

        case BadgeType.Warning:
            return Style.badgeWarning;

        case BadgeType.Info:
            return Style.badgeInfo;

        case BadgeType.Light:
            return Style.badgeLight;

        case BadgeType.Dark:
            return Style.badgeDark;
    }
}