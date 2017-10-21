import React from "react";
import { Link } from "react-router-dom";

import Style from "./license.css";

export default class GnuLesserGeneralPublicLicense extends React.PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class={Style.license}>
                <h3>GNU LESSER GENERAL PUBLIC LICENSE <small>Version 3, 29 June 2007</small></h3>

                <p>
                    Copyright (C) 2007 Free Software Foundation, Inc. <Link to="https://fsf.org/">https://fsf.org/</Link>
                </p>

                <p>
                    Everyone is permitted to copy and distribute verbatim copies of this license document, but changing it is not allowed.
                </p>

                <p>
                    This version of the GNU Lesser General Public License incorporates the terms and conditions of version 3 of the GNU General Public License, supplemented by the additional permissions listed below.
                </p>

                <h4>0. Additional Definitions.</h4>

                <p>
                    As used herein, &quot;this License&quot; refers to version 3 of the GNU Lesser General Public License, and the &quot;GNU GPL&quot; refers to version 3 of the GNU General Public License.
                </p>

                <p>
                    &quot;The Library&quot; refers to a covered work governed by this License, other than an Application or a Combined Work as defined below.
                </p>

                <p>
                    An &quot;Application&quot; is any work that makes use of an interface provided by the Library, but which is not otherwise based on the Library. Defining a subclass of a class defined by the Library is deemed a mode of using an interface provided by the Library.
                </p>

                <p>
                    A &quot;Combined Work&quot; is a work produced by combining or linking an Application with the Library. The particular version of the Library with which the Combined Work was made is also called the &quot;Linked Version&quot;.
                </p>

                <p>
                    The &quot;Minimal Corresponding Source&quot; for a Combined Work means the Corresponding Source for the Combined Work, excluding any source code for portions of the Combined Work that, considered in isolation, are based on the Application, and not on the Linked Version.
                </p>

                <p>
                    The &quot;Corresponding Application Code&quot; for a Combined Work means the object code and/or source code for the Application, including any data and utility programs needed for reproducing the Combined Work from the Application, but excluding the System Libraries of the Combined Work.
                </p>

                <h4>1. Exception to Section 3 of the GNU GPL.</h4>

                <p>
                    You may convey a covered work under sections 3 and 4 of this License without being bound by section 3 of the GNU GPL.
                </p>

                <h4>2. Conveying Modified Versions.</h4>

                <p>
                    If you modify a copy of the Library, and, in your modifications, a facility refers to a function or data to be supplied by an Application that uses the facility (other than as an argument passed when the facility is invoked), then you may convey a copy of the modified version:
                </p>

                <ol type="a">
                    <li>
                        under this License, provided that you make a good faith effort to ensure that, in the event an Application does not supply the function or data, the facility still operates, and performs whatever part of its purpose remains meaningful, or
                    </li>
                    <li>
                        under the GNU GPL, with none of the additional permissions of this License applicable to that copy.
                    </li>
                </ol>

                <h4>3. Object Code Incorporating Material from Library Header Files.</h4>

                <p>
                    The object code form of an Application may incorporate material from a header file that is part of the Library. You may convey such object code under terms of your choice, provided that, if the incorporated material is not limited to numerical parameters, data structure layouts and accessors, or small macros, inline functions and templates (ten or fewer lines in length), you do both of the following:
                </p>

                <ol type="a">
                    <li>
                        Give prominent notice with each copy of the object code that the Library is used in it and that the Library and its use are covered by this License.
                    </li>
                    <li>
                        Accompany the object code with a copy of the GNU GPL and this license document.
                    </li>
                </ol>

                <h4>4. Combined Works.</h4>

                <p>
                    You may convey a Combined Work under terms of your choice that, taken together, effectively do not restrict modification of the portions of the Library contained in the Combined Work and reverse engineering for debugging such modifications, if you also do each of the following:
                </p>
                <ol type="a">
                    <li>
                        Give prominent notice with each copy of the Combined Work that the Library is used in it and that the Library and its use are covered by this License.
                    </li>
                    <li>
                        Accompany the Combined Work with a copy of the GNU GPL and this license document.
                    </li>
                    <li>
                        For a Combined Work that displays copyright notices during execution, include the copyright notice for the Library among these notices, as well as a reference directing the user to the copies of the GNU GPL and this license document.
                    </li>
                    <li>
                        Do one of the following:
                        <ol start="0">
                            <li>
                                Convey the Minimal Corresponding Source under the terms of this License, and the Corresponding Application Code in a form suitable for, and under terms that permit, the user to recombine or relink the Application with a modified version of the Linked Version to produce a modified Combined Work, in the manner specified by section 6 of the GNU GPL for conveying Corresponding Source.
                            </li>
                            <li>
                                Use a suitable shared library mechanism for linking with the Library. A suitable mechanism is one that (a) uses at run time a copy of the Library already present on the user's computer system, and (b) will operate properly with a modified version of the Library that is interface-compatible with the Linked Version.
                            </li>
                        </ol>
                    </li>
                    <li>
                        Provide Installation Information, but only if you would otherwise be required to provide such information under section 6 of the GNU GPL, and only to the extent that such information is necessary to install and execute a modified version of the Combined Work produced by recombining or relinking the Application with a modified version of the Linked Version. (If you use option 4d0, the Installation Information must accompany the Minimal Corresponding Source and Corresponding Application Code. If you use option 4d1, you must provide the Installation Information in the manner specified by section 6 of the GNU GPL for conveying Corresponding Source.)
                    </li>
                </ol>

                <h4>5. Combined Libraries.</h4>

                <p>
                    You may place library facilities that are a work based on the Library side by side in a single library together with other library facilities that are not Applications and are not covered by this License, and convey such a combined library under terms of your choice, if you do both of the following:
                </p>

                <ol type="a">
                    <li>
                        Accompany the combined library with a copy of the same work based on the Library, uncombined with any other library facilities, conveyed under the terms of this License.
                    </li>
                    <li>
                        Give prominent notice with the combined library that part of it is a work based on the Library, and explaining where to find the accompanying uncombined form of the same work.
                    </li>
                </ol>

                <h4>6. Revised Versions of the GNU Lesser General Public License.</h4>

                <p>
                    The Free Software Foundation may publish revised and/or new versions of the GNU Lesser General Public License from time to time. Such new versions will be similar in spirit to the present version, but may differ in detail to address new problems or concerns.
                </p>
                <p>
                    Each version is given a distinguishing version number. If the Library as you received it specifies that a certain numbered version of the GNU Lesser General Public License &quot;or any later version&quot; applies to it, you have the option of following the terms and conditions either of that published version or of any later version published by the Free Software Foundation. If the Library as you received it does not specify a version number of the GNU Lesser General Public License, you may choose any version of the GNU Lesser General Public License ever published by the Free Software Foundation.
                </p>
                <p>
                    If the Library as you received it specifies that a proxy can decide whether future versions of the GNU Lesser General Public License shall apply, that proxy's public statement of acceptance of any version is permanent authorization for you to choose that version for the Library.
                </p>
            </div>
        );
    }
}