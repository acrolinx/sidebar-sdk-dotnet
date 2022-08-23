(function(){function r(e,n,t){function o(i,f){if(!n[i]){if(!e[i]){var c="function"==typeof require&&require;if(!f&&c)return c(i,!0);if(u)return u(i,!0);var a=new Error("Cannot find module '"+i+"'");throw a.code="MODULE_NOT_FOUND",a}var p=n[i]={exports:{}};e[i][0].call(p.exports,function(r){var n=e[i][1][r];return o(n||r)},p,p.exports,r,e,n,t)}return n[i].exports}for(var u="function"==typeof require&&require,i=0;i<t.length;i++)o(t[i]);return o}return r})()({1:[function(require,module,exports){
"use strict";
/*
 * Copyright 2016-present Acrolinx GmbH
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
Object.defineProperty(exports, "__esModule", { value: true });
exports.LogEntryType = exports.MessageType = exports.SoftwareComponentCategory = void 0;
exports.SoftwareComponentCategory = {
    /**
     * There should be exactly one MAIN component.
     * This information is used to identify your client on the server.
     * Version information about this components might be displayed more prominently.
     */
    MAIN: 'MAIN',
    /**
     * Version information about such components are displayed in the about
     * dialog.
     */
    DEFAULT: 'DEFAULT',
    /**
     * Version information about such components are displayed in the detail section of the about
     * dialog or not at all.
     */
    DETAIL: 'DETAIL'
};
var MessageType;
(function (MessageType) {
    MessageType["success"] = "success";
    MessageType["info"] = "info";
    MessageType["warning"] = "warning";
    MessageType["error"] = "error";
})(MessageType = exports.MessageType || (exports.MessageType = {}));
var LogEntryType;
(function (LogEntryType) {
    LogEntryType["debug"] = "debug";
    LogEntryType["info"] = "info";
    LogEntryType["warning"] = "warning";
    LogEntryType["error"] = "error";
})(LogEntryType = exports.LogEntryType || (exports.LogEntryType = {}));

},{}],2:[function(require,module,exports){
var n,l,u,t,i,r,o,f,e={},c=[],s=/acit|ex(?:s|g|n|p|$)|rph|grid|ows|mnc|ntw|ine[ch]|zoo|^ord|itera/i;function a(n,l){for(var u in l)n[u]=l[u];return n}function v(n){var l=n.parentNode;l&&l.removeChild(n)}function h(l,u,t){var i,r,o,f={};for(o in u)"key"==o?i=u[o]:"ref"==o?r=u[o]:f[o]=u[o];if(arguments.length>2&&(f.children=arguments.length>3?n.call(arguments,2):t),"function"==typeof l&&null!=l.defaultProps)for(o in l.defaultProps)void 0===f[o]&&(f[o]=l.defaultProps[o]);return p(l,f,i,r,null)}function p(n,t,i,r,o){var f={type:n,props:t,key:i,ref:r,__k:null,__:null,__b:0,__e:null,__d:void 0,__c:null,__h:null,constructor:void 0,__v:null==o?++u:o};return null==o&&null!=l.vnode&&l.vnode(f),f}function y(n){return n.children}function d(n,l){this.props=n,this.context=l}function _(n,l){if(null==l)return n.__?_(n.__,n.__.__k.indexOf(n)+1):null;for(var u;l<n.__k.length;l++)if(null!=(u=n.__k[l])&&null!=u.__e)return u.__e;return"function"==typeof n.type?_(n):null}function k(n){var l,u;if(null!=(n=n.__)&&null!=n.__c){for(n.__e=n.__c.base=null,l=0;l<n.__k.length;l++)if(null!=(u=n.__k[l])&&null!=u.__e){n.__e=n.__c.base=u.__e;break}return k(n)}}function x(n){(!n.__d&&(n.__d=!0)&&i.push(n)&&!b.__r++||o!==l.debounceRendering)&&((o=l.debounceRendering)||r)(b)}function b(){for(var n;b.__r=i.length;)n=i.sort(function(n,l){return n.__v.__b-l.__v.__b}),i=[],n.some(function(n){var l,u,t,i,r,o;n.__d&&(r=(i=(l=n).__v).__e,(o=l.__P)&&(u=[],(t=a({},i)).__v=i.__v+1,I(o,i,t,l.__n,void 0!==o.ownerSVGElement,null!=i.__h?[r]:null,u,null==r?_(i):r,i.__h),T(u,i),i.__e!=r&&k(i)))})}function m(n,l,u,t,i,r,o,f,s,a){var v,h,d,k,x,b,m,A=t&&t.__k||c,P=A.length;for(u.__k=[],v=0;v<l.length;v++)if(null!=(k=u.__k[v]=null==(k=l[v])||"boolean"==typeof k?null:"string"==typeof k||"number"==typeof k||"bigint"==typeof k?p(null,k,null,null,k):Array.isArray(k)?p(y,{children:k},null,null,null):k.__b>0?p(k.type,k.props,k.key,null,k.__v):k)){if(k.__=u,k.__b=u.__b+1,null===(d=A[v])||d&&k.key==d.key&&k.type===d.type)A[v]=void 0;else for(h=0;h<P;h++){if((d=A[h])&&k.key==d.key&&k.type===d.type){A[h]=void 0;break}d=null}I(n,k,d=d||e,i,r,o,f,s,a),x=k.__e,(h=k.ref)&&d.ref!=h&&(m||(m=[]),d.ref&&m.push(d.ref,null,k),m.push(h,k.__c||x,k)),null!=x?(null==b&&(b=x),"function"==typeof k.type&&k.__k===d.__k?k.__d=s=g(k,s,n):s=w(n,k,d,A,x,s),"function"==typeof u.type&&(u.__d=s)):s&&d.__e==s&&s.parentNode!=n&&(s=_(d))}for(u.__e=b,v=P;v--;)null!=A[v]&&("function"==typeof u.type&&null!=A[v].__e&&A[v].__e==u.__d&&(u.__d=_(t,v+1)),L(A[v],A[v]));if(m)for(v=0;v<m.length;v++)z(m[v],m[++v],m[++v])}function g(n,l,u){for(var t,i=n.__k,r=0;i&&r<i.length;r++)(t=i[r])&&(t.__=n,l="function"==typeof t.type?g(t,l,u):w(u,t,t,i,t.__e,l));return l}function w(n,l,u,t,i,r){var o,f,e;if(void 0!==l.__d)o=l.__d,l.__d=void 0;else if(null==u||i!=r||null==i.parentNode)n:if(null==r||r.parentNode!==n)n.appendChild(i),o=null;else{for(f=r,e=0;(f=f.nextSibling)&&e<t.length;e+=2)if(f==i)break n;n.insertBefore(i,r),o=r}return void 0!==o?o:i.nextSibling}function A(n,l,u,t,i){var r;for(r in u)"children"===r||"key"===r||r in l||C(n,r,null,u[r],t);for(r in l)i&&"function"!=typeof l[r]||"children"===r||"key"===r||"value"===r||"checked"===r||u[r]===l[r]||C(n,r,l[r],u[r],t)}function P(n,l,u){"-"===l[0]?n.setProperty(l,u):n[l]=null==u?"":"number"!=typeof u||s.test(l)?u:u+"px"}function C(n,l,u,t,i){var r;n:if("style"===l)if("string"==typeof u)n.style.cssText=u;else{if("string"==typeof t&&(n.style.cssText=t=""),t)for(l in t)u&&l in u||P(n.style,l,"");if(u)for(l in u)t&&u[l]===t[l]||P(n.style,l,u[l])}else if("o"===l[0]&&"n"===l[1])r=l!==(l=l.replace(/Capture$/,"")),l=l.toLowerCase()in n?l.toLowerCase().slice(2):l.slice(2),n.l||(n.l={}),n.l[l+r]=u,u?t||n.addEventListener(l,r?H:$,r):n.removeEventListener(l,r?H:$,r);else if("dangerouslySetInnerHTML"!==l){if(i)l=l.replace(/xlink[H:h]/,"h").replace(/sName$/,"s");else if("href"!==l&&"list"!==l&&"form"!==l&&"tabIndex"!==l&&"download"!==l&&l in n)try{n[l]=null==u?"":u;break n}catch(n){}"function"==typeof u||(null!=u&&(!1!==u||"a"===l[0]&&"r"===l[1])?n.setAttribute(l,u):n.removeAttribute(l))}}function $(n){this.l[n.type+!1](l.event?l.event(n):n)}function H(n){this.l[n.type+!0](l.event?l.event(n):n)}function I(n,u,t,i,r,o,f,e,c){var s,v,h,p,_,k,x,b,g,w,A,P=u.type;if(void 0!==u.constructor)return null;null!=t.__h&&(c=t.__h,e=u.__e=t.__e,u.__h=null,o=[e]),(s=l.__b)&&s(u);try{n:if("function"==typeof P){if(b=u.props,g=(s=P.contextType)&&i[s.__c],w=s?g?g.props.value:s.__:i,t.__c?x=(v=u.__c=t.__c).__=v.__E:("prototype"in P&&P.prototype.render?u.__c=v=new P(b,w):(u.__c=v=new d(b,w),v.constructor=P,v.render=M),g&&g.sub(v),v.props=b,v.state||(v.state={}),v.context=w,v.__n=i,h=v.__d=!0,v.__h=[]),null==v.__s&&(v.__s=v.state),null!=P.getDerivedStateFromProps&&(v.__s==v.state&&(v.__s=a({},v.__s)),a(v.__s,P.getDerivedStateFromProps(b,v.__s))),p=v.props,_=v.state,h)null==P.getDerivedStateFromProps&&null!=v.componentWillMount&&v.componentWillMount(),null!=v.componentDidMount&&v.__h.push(v.componentDidMount);else{if(null==P.getDerivedStateFromProps&&b!==p&&null!=v.componentWillReceiveProps&&v.componentWillReceiveProps(b,w),!v.__e&&null!=v.shouldComponentUpdate&&!1===v.shouldComponentUpdate(b,v.__s,w)||u.__v===t.__v){v.props=b,v.state=v.__s,u.__v!==t.__v&&(v.__d=!1),v.__v=u,u.__e=t.__e,u.__k=t.__k,u.__k.forEach(function(n){n&&(n.__=u)}),v.__h.length&&f.push(v);break n}null!=v.componentWillUpdate&&v.componentWillUpdate(b,v.__s,w),null!=v.componentDidUpdate&&v.__h.push(function(){v.componentDidUpdate(p,_,k)})}v.context=w,v.props=b,v.state=v.__s,(s=l.__r)&&s(u),v.__d=!1,v.__v=u,v.__P=n,s=v.render(v.props,v.state,v.context),v.state=v.__s,null!=v.getChildContext&&(i=a(a({},i),v.getChildContext())),h||null==v.getSnapshotBeforeUpdate||(k=v.getSnapshotBeforeUpdate(p,_)),A=null!=s&&s.type===y&&null==s.key?s.props.children:s,m(n,Array.isArray(A)?A:[A],u,t,i,r,o,f,e,c),v.base=u.__e,u.__h=null,v.__h.length&&f.push(v),x&&(v.__E=v.__=null),v.__e=!1}else null==o&&u.__v===t.__v?(u.__k=t.__k,u.__e=t.__e):u.__e=j(t.__e,u,t,i,r,o,f,c);(s=l.diffed)&&s(u)}catch(n){u.__v=null,(c||null!=o)&&(u.__e=e,u.__h=!!c,o[o.indexOf(e)]=null),l.__e(n,u,t)}}function T(n,u){l.__c&&l.__c(u,n),n.some(function(u){try{n=u.__h,u.__h=[],n.some(function(n){n.call(u)})}catch(n){l.__e(n,u.__v)}})}function j(l,u,t,i,r,o,f,c){var s,a,h,p=t.props,y=u.props,d=u.type,k=0;if("svg"===d&&(r=!0),null!=o)for(;k<o.length;k++)if((s=o[k])&&(s===l||(d?s.localName==d:3==s.nodeType))){l=s,o[k]=null;break}if(null==l){if(null===d)return document.createTextNode(y);l=r?document.createElementNS("http://www.w3.org/2000/svg",d):document.createElement(d,y.is&&y),o=null,c=!1}if(null===d)p===y||c&&l.data===y||(l.data=y);else{if(o=o&&n.call(l.childNodes),a=(p=t.props||e).dangerouslySetInnerHTML,h=y.dangerouslySetInnerHTML,!c){if(null!=o)for(p={},k=0;k<l.attributes.length;k++)p[l.attributes[k].name]=l.attributes[k].value;(h||a)&&(h&&(a&&h.__html==a.__html||h.__html===l.innerHTML)||(l.innerHTML=h&&h.__html||""))}if(A(l,y,p,r,c),h)u.__k=[];else if(k=u.props.children,m(l,Array.isArray(k)?k:[k],u,t,i,r&&"foreignObject"!==d,o,f,o?o[0]:t.__k&&_(t,0),c),null!=o)for(k=o.length;k--;)null!=o[k]&&v(o[k]);c||("value"in y&&void 0!==(k=y.value)&&(k!==l.value||"progress"===d&&!k)&&C(l,"value",k,p.value,!1),"checked"in y&&void 0!==(k=y.checked)&&k!==l.checked&&C(l,"checked",k,p.checked,!1))}return l}function z(n,u,t){try{"function"==typeof n?n(u):n.current=u}catch(n){l.__e(n,t)}}function L(n,u,t){var i,r;if(l.unmount&&l.unmount(n),(i=n.ref)&&(i.current&&i.current!==n.__e||z(i,null,u)),null!=(i=n.__c)){if(i.componentWillUnmount)try{i.componentWillUnmount()}catch(n){l.__e(n,u)}i.base=i.__P=null}if(i=n.__k)for(r=0;r<i.length;r++)i[r]&&L(i[r],u,"function"!=typeof n.type);t||null==n.__e||v(n.__e),n.__e=n.__d=void 0}function M(n,l,u){return this.constructor(n,u)}function N(u,t,i){var r,o,f;l.__&&l.__(u,t),o=(r="function"==typeof i)?null:i&&i.__k||t.__k,f=[],I(t,u=(!r&&i||t).__k=h(y,null,[u]),o||e,e,void 0!==t.ownerSVGElement,!r&&i?[i]:o?null:t.firstChild?n.call(t.childNodes):null,f,!r&&i?i:o?o.__e:t.firstChild,r),T(f,u)}n=c.slice,l={__e:function(n,l){for(var u,t,i;l=l.__;)if((u=l.__c)&&!u.__)try{if((t=u.constructor)&&null!=t.getDerivedStateFromError&&(u.setState(t.getDerivedStateFromError(n)),i=u.__d),null!=u.componentDidCatch&&(u.componentDidCatch(n),i=u.__d),i)return u.__E=u}catch(l){n=l}throw n}},u=0,t=function(n){return null!=n&&void 0===n.constructor},d.prototype.setState=function(n,l){var u;u=null!=this.__s&&this.__s!==this.state?this.__s:this.__s=a({},this.state),"function"==typeof n&&(n=n(a({},u),this.props)),n&&a(u,n),null!=n&&this.__v&&(l&&this.__h.push(l),x(this))},d.prototype.forceUpdate=function(n){this.__v&&(this.__e=!0,n&&this.__h.push(n),x(this))},d.prototype.render=y,i=[],r="function"==typeof Promise?Promise.prototype.then.bind(Promise.resolve()):setTimeout,b.__r=0,f=0,exports.render=N,exports.hydrate=function n(l,u){N(l,u,n)},exports.createElement=h,exports.h=h,exports.Fragment=y,exports.createRef=function(){return{current:null}},exports.isValidElement=t,exports.Component=d,exports.cloneElement=function(l,u,t){var i,r,o,f=a({},l.props);for(o in u)"key"==o?i=u[o]:"ref"==o?r=u[o]:f[o]=u[o];return arguments.length>2&&(f.children=arguments.length>3?n.call(arguments,2):t),p(l.type,f,i||l.key,r||l.ref,null)},exports.createContext=function(n,l){var u={__c:l="__cC"+f++,__:n,Consumer:function(n,l){return n.children(l)},Provider:function(n){var u,t;return this.getChildContext||(u=[],(t={})[l]=this,this.getChildContext=function(){return t},this.shouldComponentUpdate=function(n){this.props.value!==n.value&&u.some(x)},this.sub=function(n){u.push(n);var l=n.componentWillUnmount;n.componentWillUnmount=function(){u.splice(u.indexOf(n),1),l&&l.call(n)}}),n.children}};return u.Provider.__=u.Consumer.contextType=u},exports.toChildArray=function n(l,u){return u=u||[],null==l||"boolean"==typeof l||(Array.isArray(l)?l.some(function(l){n(l,u)}):u.push(l)),u},exports.options=l;


},{}],3:[function(require,module,exports){
(function (global){(function (){
/*! *****************************************************************************
Copyright (c) Microsoft Corporation.

Permission to use, copy, modify, and/or distribute this software for any
purpose with or without fee is hereby granted.

THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES WITH
REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY
AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT,
INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM
LOSS OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR
OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR
PERFORMANCE OF THIS SOFTWARE.
***************************************************************************** */
/* global global, define, System, Reflect, Promise */
var __extends;
var __assign;
var __rest;
var __decorate;
var __param;
var __metadata;
var __awaiter;
var __generator;
var __exportStar;
var __values;
var __read;
var __spread;
var __spreadArrays;
var __spreadArray;
var __await;
var __asyncGenerator;
var __asyncDelegator;
var __asyncValues;
var __makeTemplateObject;
var __importStar;
var __importDefault;
var __classPrivateFieldGet;
var __classPrivateFieldSet;
var __createBinding;
(function (factory) {
    var root = typeof global === "object" ? global : typeof self === "object" ? self : typeof this === "object" ? this : {};
    if (typeof define === "function" && define.amd) {
        define("tslib", ["exports"], function (exports) { factory(createExporter(root, createExporter(exports))); });
    }
    else if (typeof module === "object" && typeof module.exports === "object") {
        factory(createExporter(root, createExporter(module.exports)));
    }
    else {
        factory(createExporter(root));
    }
    function createExporter(exports, previous) {
        if (exports !== root) {
            if (typeof Object.create === "function") {
                Object.defineProperty(exports, "__esModule", { value: true });
            }
            else {
                exports.__esModule = true;
            }
        }
        return function (id, v) { return exports[id] = previous ? previous(id, v) : v; };
    }
})
(function (exporter) {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };

    __extends = function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };

    __assign = Object.assign || function (t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p)) t[p] = s[p];
        }
        return t;
    };

    __rest = function (s, e) {
        var t = {};
        for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p) && e.indexOf(p) < 0)
            t[p] = s[p];
        if (s != null && typeof Object.getOwnPropertySymbols === "function")
            for (var i = 0, p = Object.getOwnPropertySymbols(s); i < p.length; i++) {
                if (e.indexOf(p[i]) < 0 && Object.prototype.propertyIsEnumerable.call(s, p[i]))
                    t[p[i]] = s[p[i]];
            }
        return t;
    };

    __decorate = function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };

    __param = function (paramIndex, decorator) {
        return function (target, key) { decorator(target, key, paramIndex); }
    };

    __metadata = function (metadataKey, metadataValue) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(metadataKey, metadataValue);
    };

    __awaiter = function (thisArg, _arguments, P, generator) {
        function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
        return new (P || (P = Promise))(function (resolve, reject) {
            function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
            function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
            function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
            step((generator = generator.apply(thisArg, _arguments || [])).next());
        });
    };

    __generator = function (thisArg, body) {
        var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
        return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
        function verb(n) { return function (v) { return step([n, v]); }; }
        function step(op) {
            if (f) throw new TypeError("Generator is already executing.");
            while (_) try {
                if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
                if (y = 0, t) op = [op[0] & 2, t.value];
                switch (op[0]) {
                    case 0: case 1: t = op; break;
                    case 4: _.label++; return { value: op[1], done: false };
                    case 5: _.label++; y = op[1]; op = [0]; continue;
                    case 7: op = _.ops.pop(); _.trys.pop(); continue;
                    default:
                        if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                        if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                        if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                        if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                        if (t[2]) _.ops.pop();
                        _.trys.pop(); continue;
                }
                op = body.call(thisArg, _);
            } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
            if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
        }
    };

    __exportStar = function(m, o) {
        for (var p in m) if (p !== "default" && !Object.prototype.hasOwnProperty.call(o, p)) __createBinding(o, m, p);
    };

    __createBinding = Object.create ? (function(o, m, k, k2) {
        if (k2 === undefined) k2 = k;
        Object.defineProperty(o, k2, { enumerable: true, get: function() { return m[k]; } });
    }) : (function(o, m, k, k2) {
        if (k2 === undefined) k2 = k;
        o[k2] = m[k];
    });

    __values = function (o) {
        var s = typeof Symbol === "function" && Symbol.iterator, m = s && o[s], i = 0;
        if (m) return m.call(o);
        if (o && typeof o.length === "number") return {
            next: function () {
                if (o && i >= o.length) o = void 0;
                return { value: o && o[i++], done: !o };
            }
        };
        throw new TypeError(s ? "Object is not iterable." : "Symbol.iterator is not defined.");
    };

    __read = function (o, n) {
        var m = typeof Symbol === "function" && o[Symbol.iterator];
        if (!m) return o;
        var i = m.call(o), r, ar = [], e;
        try {
            while ((n === void 0 || n-- > 0) && !(r = i.next()).done) ar.push(r.value);
        }
        catch (error) { e = { error: error }; }
        finally {
            try {
                if (r && !r.done && (m = i["return"])) m.call(i);
            }
            finally { if (e) throw e.error; }
        }
        return ar;
    };

    /** @deprecated */
    __spread = function () {
        for (var ar = [], i = 0; i < arguments.length; i++)
            ar = ar.concat(__read(arguments[i]));
        return ar;
    };

    /** @deprecated */
    __spreadArrays = function () {
        for (var s = 0, i = 0, il = arguments.length; i < il; i++) s += arguments[i].length;
        for (var r = Array(s), k = 0, i = 0; i < il; i++)
            for (var a = arguments[i], j = 0, jl = a.length; j < jl; j++, k++)
                r[k] = a[j];
        return r;
    };

    __spreadArray = function (to, from, pack) {
        if (pack || arguments.length === 2) for (var i = 0, l = from.length, ar; i < l; i++) {
            if (ar || !(i in from)) {
                if (!ar) ar = Array.prototype.slice.call(from, 0, i);
                ar[i] = from[i];
            }
        }
        return to.concat(ar || Array.prototype.slice.call(from));
    };

    __await = function (v) {
        return this instanceof __await ? (this.v = v, this) : new __await(v);
    };

    __asyncGenerator = function (thisArg, _arguments, generator) {
        if (!Symbol.asyncIterator) throw new TypeError("Symbol.asyncIterator is not defined.");
        var g = generator.apply(thisArg, _arguments || []), i, q = [];
        return i = {}, verb("next"), verb("throw"), verb("return"), i[Symbol.asyncIterator] = function () { return this; }, i;
        function verb(n) { if (g[n]) i[n] = function (v) { return new Promise(function (a, b) { q.push([n, v, a, b]) > 1 || resume(n, v); }); }; }
        function resume(n, v) { try { step(g[n](v)); } catch (e) { settle(q[0][3], e); } }
        function step(r) { r.value instanceof __await ? Promise.resolve(r.value.v).then(fulfill, reject) : settle(q[0][2], r);  }
        function fulfill(value) { resume("next", value); }
        function reject(value) { resume("throw", value); }
        function settle(f, v) { if (f(v), q.shift(), q.length) resume(q[0][0], q[0][1]); }
    };

    __asyncDelegator = function (o) {
        var i, p;
        return i = {}, verb("next"), verb("throw", function (e) { throw e; }), verb("return"), i[Symbol.iterator] = function () { return this; }, i;
        function verb(n, f) { i[n] = o[n] ? function (v) { return (p = !p) ? { value: __await(o[n](v)), done: n === "return" } : f ? f(v) : v; } : f; }
    };

    __asyncValues = function (o) {
        if (!Symbol.asyncIterator) throw new TypeError("Symbol.asyncIterator is not defined.");
        var m = o[Symbol.asyncIterator], i;
        return m ? m.call(o) : (o = typeof __values === "function" ? __values(o) : o[Symbol.iterator](), i = {}, verb("next"), verb("throw"), verb("return"), i[Symbol.asyncIterator] = function () { return this; }, i);
        function verb(n) { i[n] = o[n] && function (v) { return new Promise(function (resolve, reject) { v = o[n](v), settle(resolve, reject, v.done, v.value); }); }; }
        function settle(resolve, reject, d, v) { Promise.resolve(v).then(function(v) { resolve({ value: v, done: d }); }, reject); }
    };

    __makeTemplateObject = function (cooked, raw) {
        if (Object.defineProperty) { Object.defineProperty(cooked, "raw", { value: raw }); } else { cooked.raw = raw; }
        return cooked;
    };

    var __setModuleDefault = Object.create ? (function(o, v) {
        Object.defineProperty(o, "default", { enumerable: true, value: v });
    }) : function(o, v) {
        o["default"] = v;
    };

    __importStar = function (mod) {
        if (mod && mod.__esModule) return mod;
        var result = {};
        if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
        __setModuleDefault(result, mod);
        return result;
    };

    __importDefault = function (mod) {
        return (mod && mod.__esModule) ? mod : { "default": mod };
    };

    __classPrivateFieldGet = function (receiver, state, kind, f) {
        if (kind === "a" && !f) throw new TypeError("Private accessor was defined without a getter");
        if (typeof state === "function" ? receiver !== state || !f : !state.has(receiver)) throw new TypeError("Cannot read private member from an object whose class did not declare it");
        return kind === "m" ? f : kind === "a" ? f.call(receiver) : f ? f.value : state.get(receiver);
    };

    __classPrivateFieldSet = function (receiver, state, value, kind, f) {
        if (kind === "m") throw new TypeError("Private method is not writable");
        if (kind === "a" && !f) throw new TypeError("Private accessor was defined without a setter");
        if (typeof state === "function" ? receiver !== state || !f : !state.has(receiver)) throw new TypeError("Cannot write private member to an object whose class did not declare it");
        return (kind === "a" ? f.call(receiver, value) : f ? f.value = value : state.set(receiver, value)), value;
    };

    exporter("__extends", __extends);
    exporter("__assign", __assign);
    exporter("__rest", __rest);
    exporter("__decorate", __decorate);
    exporter("__param", __param);
    exporter("__metadata", __metadata);
    exporter("__awaiter", __awaiter);
    exporter("__generator", __generator);
    exporter("__exportStar", __exportStar);
    exporter("__createBinding", __createBinding);
    exporter("__values", __values);
    exporter("__read", __read);
    exporter("__spread", __spread);
    exporter("__spreadArrays", __spreadArrays);
    exporter("__spreadArray", __spreadArray);
    exporter("__await", __await);
    exporter("__asyncGenerator", __asyncGenerator);
    exporter("__asyncDelegator", __asyncDelegator);
    exporter("__asyncValues", __asyncValues);
    exporter("__makeTemplateObject", __makeTemplateObject);
    exporter("__importStar", __importStar);
    exporter("__importDefault", __importDefault);
    exporter("__classPrivateFieldGet", __classPrivateFieldGet);
    exporter("__classPrivateFieldSet", __classPrivateFieldSet);
});

}).call(this)}).call(this,typeof global !== "undefined" ? global : typeof self !== "undefined" ? self : typeof window !== "undefined" ? window : {})
},{}],4:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.createSidebarMessageProxy = void 0;
var tslib_1 = require("tslib");
function removeFunctions(object) {
    return JSON.parse(JSON.stringify(object));
}
function postCommandAsMessage(window, command) {
    var args = [];
    for (var _i = 2; _i < arguments.length; _i++) {
        args[_i - 2] = arguments[_i];
    }
    window.postMessage({
        command: command,
        args: removeFunctions(args)
    }, '*');
}
function injectPostCommandAsMessage(windowProvider, object) {
    var _loop_1 = function (key) {
        var originalMethod = object[key];
        object[key] = function () {
            var args = [];
            for (var _i = 0; _i < arguments.length; _i++) {
                args[_i] = arguments[_i];
            }
            postCommandAsMessage.apply(void 0, tslib_1.__spreadArray([windowProvider(), key], args));
            return originalMethod.apply(object, args);
        };
    };
    for (var key in object) {
        _loop_1(key);
    }
}
function createSidebarMessageProxy(sidebarWindow) {
    var sidebar = {
        init: function (_initParameters) {
        },
        configure: function (_initParameters) {
        },
        checkGlobal: function (_documentContent, _options) {
            return { checkId: 'dummyCheckId' };
        },
        onGlobalCheckRejected: function () {
        },
        invalidateRanges: function (_invalidCheckedDocumentRanges) {
        },
        onVisibleRangesChanged: function (_checkedDocumentRanges) {
        },
        showMessage: function (_message) {
        },
        setStorage: function (_props) {
        }
    };
    injectPostCommandAsMessage(function () { return sidebarWindow; }, sidebar);
    return sidebar;
}
exports.createSidebarMessageProxy = createSidebarMessageProxy;

},{"tslib":3}],5:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.loadSidebarIntoIFrame = exports.NoValidSidebarError = exports.getSidebarVersion = void 0;
var tslib_1 = require("tslib");
var utils = require("./utils");
var constants_1 = require("../../constants");
var utils_1 = require("../../utils/utils");
var logging = require("../../utils/logging");
var DEFAULT_MINIMUM_SIDEBAR_VERSION = [14, 3, 1];
function getSidebarVersion(sidebarHtml) {
    var match = sidebarHtml.match(/<meta name=\"sidebar-version\" content=\"(\d+)\.(\d+)\.(\d+)/);
    if (!match || match.length !== 4) {
        return null;
    }
    var versionParts = match.slice(1, 4).map(function (s) { return parseInt(s); });
    return [versionParts[0], versionParts[1], versionParts[2]];
}
exports.getSidebarVersion = getSidebarVersion;
var NoValidSidebarError = (function (_super) {
    tslib_1.__extends(NoValidSidebarError, _super);
    function NoValidSidebarError(acrolinxErrorCode, message, url) {
        var _this = _super.call(this, message) || this;
        _this.acrolinxErrorCode = acrolinxErrorCode;
        _this.url = url;
        return _this;
    }
    return NoValidSidebarError;
}(Error));
exports.NoValidSidebarError = NoValidSidebarError;
function loadSidebarIntoIFrame(config, sidebarIFrameElement, onSidebarLoaded) {
    logging.log('loadSidebarIntoIFrame', config);
    var sidebarBaseUrl = constants_1.FORCE_SIDEBAR_URL || config.sidebarUrl;
    var completeSidebarUrl = sidebarBaseUrl + 'index.html?t=' + Date.now();
    utils.fetch(completeSidebarUrl, { timeout: 10000 }, function (sidebarHtmlOrError) {
        if (typeof sidebarHtmlOrError !== 'string') {
            var fetchError = sidebarHtmlOrError;
            logging.error('Error while fetching the sidebar: ' + fetchError.acrolinxErrorCode, fetchError);
            onSidebarLoaded(fetchError);
            return;
        }
        var sidebarHtml = sidebarHtmlOrError;
        if (sidebarHtml.indexOf('<meta name="sidebar-version"') < 0) {
            onSidebarLoaded(new NoValidSidebarError('noSidebar', 'No valid sidebar html code:' + sidebarHtml, completeSidebarUrl));
            return;
        }
        var sidebarVersion = getSidebarVersion(sidebarHtml);
        var wrongSidebarVersion = !sidebarVersion
            || !utils_1.isVersionGreaterEqual(sidebarVersion, DEFAULT_MINIMUM_SIDEBAR_VERSION)
            || !utils_1.isVersionGreaterEqual(sidebarVersion, config.minimumSidebarVersion);
        if (!constants_1.FORCE_SIDEBAR_URL && wrongSidebarVersion) {
            logging.warn("Found sidebar version " + formatVersion(sidebarVersion) + " " +
                ("(default minimumSidebarVersion=" + formatVersion(DEFAULT_MINIMUM_SIDEBAR_VERSION) + ", configured minimumSidebarVersion=" + formatVersion(config.minimumSidebarVersion) + ")"));
            onSidebarLoaded(new NoValidSidebarError('sidebarVersionIsBelowMinimum', 'Sidebar version is smaller than minimumSidebarVersion', completeSidebarUrl));
            return;
        }
        logging.log('Sidebar HTML is loaded successfully');
        config.timeoutWatcher.start();
        if (config.useMessageAdapter) {
            var onLoadHandler_1 = function () {
                sidebarIFrameElement.removeEventListener('load', onLoadHandler_1);
                onSidebarLoaded();
            };
            sidebarIFrameElement.addEventListener('load', onLoadHandler_1);
            sidebarIFrameElement.src = completeSidebarUrl + '&acrolinxUseMessageApi=true';
        }
        else {
            writeSidebarHtmlIntoIFrame(sidebarHtml, sidebarIFrameElement, sidebarBaseUrl);
            onSidebarLoaded();
        }
    });
}
exports.loadSidebarIntoIFrame = loadSidebarIntoIFrame;
function formatVersion(version) {
    return version && version.join('.');
}
function writeSidebarHtmlIntoIFrame(sidebarHtml, sidebarIFrameElement, sidebarBaseUrl) {
    var sidebarContentWindow = sidebarIFrameElement.contentWindow;
    var sidebarHtmlWithAbsoluteLinks = sidebarHtml
        .replace(/src="/g, 'src="' + sidebarBaseUrl)
        .replace(/href="/g, 'href="' + sidebarBaseUrl);
    sidebarContentWindow.document.open();
    sidebarContentWindow.document.write(sidebarHtmlWithAbsoluteLinks);
    sidebarContentWindow.document.close();
}

},{"../../constants":12,"../../utils/logging":22,"../../utils/utils":25,"./utils":6,"tslib":3}],6:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.fetch = exports.FetchError = exports.isCorsWithCredentialsNeeded = void 0;
var tslib_1 = require("tslib");
var IS_WITH_CREDENTIALS_NEEDED = /^https:\/\/[a-z-_]+\.(corp\.google\.com|gcpnode\.com|corp\.goog)(:[0-9]+)?/;
function isCorsWithCredentialsNeeded(url) {
    return IS_WITH_CREDENTIALS_NEEDED.test(url);
}
exports.isCorsWithCredentialsNeeded = isCorsWithCredentialsNeeded;
var FetchError = (function (_super) {
    tslib_1.__extends(FetchError, _super);
    function FetchError(acrolinxErrorCode, message, url) {
        var _this = _super.call(this, message) || this;
        _this.acrolinxErrorCode = acrolinxErrorCode;
        _this.url = url;
        return _this;
    }
    return FetchError;
}(Error));
exports.FetchError = FetchError;
function fetch(url, opts, callback) {
    try {
        var request_1 = new XMLHttpRequest();
        request_1.open('GET', url, true);
        request_1.onload = function () {
            if (request_1.status >= 200 && request_1.status < 400) {
                callback(request_1.responseText);
            }
            else {
                callback(new FetchError('httpErrorStatus', "Error while loading " + url + ". Status = " + request_1.status, url));
            }
        };
        if (opts.timeout) {
            request_1.timeout = opts.timeout;
        }
        request_1.ontimeout = function () {
            callback(new FetchError('timeout', "Timeout while loading " + url + ".", url));
        };
        request_1.onerror = function () {
            callback(new FetchError('connectionError', "Error while loading " + url + ".", url));
        };
        request_1.withCredentials = isCorsWithCredentialsNeeded(url);
        request_1.send();
    }
    catch (error) {
        callback(new FetchError('connectionError', error.message, url));
    }
}
exports.fetch = fetch;

},{"tslib":3}],7:[function(require,module,exports){
"use strict";
var _a;
Object.defineProperty(exports, "__esModule", { value: true });
exports.aboutComponent = void 0;
var tslib_1 = require("tslib");
var preact_1 = require("preact");
var init_parameters_1 = require("../init-parameters");
var hacks_1 = require("../utils/hacks");
var preact_2 = require("../utils/preact");
var localization_1 = require("../localization");
var sidebar_interface_1 = require("@acrolinx/sidebar-interface");
var utils_1 = require("../utils/utils");
var help_link_1 = require("./help-link");
function aboutInfoLine(component) {
    return preact_2.div({ className: 'about-item', key: component.id }, preact_2.div({ className: 'about-tab-label' }, component.name), preact_2.div({ className: 'about-tab-value', title: component.version }, component.version));
}
var sortKeyByCategory = (_a = {},
    _a[sidebar_interface_1.SoftwareComponentCategory.MAIN] = '1',
    _a[sidebar_interface_1.SoftwareComponentCategory.DEFAULT] = '2',
    _a[sidebar_interface_1.SoftwareComponentCategory.DETAIL] = '3',
    _a);
function getSortKey(softwareComponent) {
    var prefix = sortKeyByCategory[softwareComponent.category || 'DEFAULT'];
    return prefix + softwareComponent.name.toLowerCase();
}
function getAdditionalComponents(logFileLocation) {
    var t = localization_1.getTranslation().serverSelector;
    var additionalComponents = [
        {
            id: 'com.acrolinx.userAgent',
            name: t.aboutItems.browserInformation,
            version: navigator.userAgent,
            category: sidebar_interface_1.SoftwareComponentCategory.DEFAULT
        },
        {
            id: "com.acrolinx.startPageCorsOrigin",
            name: t.aboutItems.startPageCorsOrigin,
            version: utils_1.getCorsOrigin(),
            category: sidebar_interface_1.SoftwareComponentCategory.DEFAULT
        }
    ];
    if (logFileLocation) {
        additionalComponents.push({
            id: "com.acrolinx.logFileLocation",
            name: t.aboutItems.logFileLocation,
            version: logFileLocation,
            category: sidebar_interface_1.SoftwareComponentCategory.DEFAULT
        });
    }
    return additionalComponents;
}
var AboutComponent = (function (_super) {
    tslib_1.__extends(AboutComponent, _super);
    function AboutComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    AboutComponent.prototype.componentDidMount = function () {
        hacks_1.forceRedrawInWebkit();
    };
    AboutComponent.prototype.render = function () {
        var t = localization_1.getTranslation().serverSelector;
        var props = this.props;
        var saneClientComponents = this.props.clientComponents.map(init_parameters_1.sanitizeClientComponent);
        var allComponentsSorted = utils_1.sortBy(saneClientComponents.concat(getAdditionalComponents(props.logFileLocation)), getSortKey);
        return preact_2.div({ className: 'aboutComponent' }, preact_2.div({
            className: preact_2.classNames('aboutHeader'),
            onClick: props.onBack,
        }, preact_2.span({ className: 'icon-arrow-back' }), help_link_1.helpLink(props)), preact_2.div({ className: 'aboutBody' }, preact_2.div({ className: 'aboutMain' }, preact_2.h1({}, t.title.about), allComponentsSorted.map(aboutInfoLine))), preact_2.div({ className: 'aboutFooter' }, props.logFileLocation ?
            preact_2.div({ className: 'buttonGroup logFileSection' }, preact_2.span({ className: 'openLogFileTitle' }, t.title.logFile), preact_2.button({
                className: "submitButton",
                onClick: props.openLogFile
            }, t.button.openLogFile))
            : []));
    };
    return AboutComponent;
}(preact_1.Component));
exports.aboutComponent = preact_2.createPreactFactory(AboutComponent);

},{"../init-parameters":13,"../localization":14,"../utils/hacks":21,"../utils/preact":23,"../utils/utils":25,"./help-link":10,"@acrolinx/sidebar-interface":1,"preact":2,"tslib":3}],8:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.errorMessageComponent = void 0;
var tslib_1 = require("tslib");
var preact_1 = require("preact");
var preact_2 = require("../utils/preact");
var localization_1 = require("../localization");
var ErrorMessageComponent = (function (_super) {
    tslib_1.__extends(ErrorMessageComponent, _super);
    function ErrorMessageComponent() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.state = {
            showDetails: false
        };
        _this.toggleErrorDetails = function (event) {
            event.preventDefault();
            _this.setState({
                showDetails: !_this.state.showDetails
            });
        };
        _this.selectDetailMessage = function (event) {
            event.preventDefault();
            var selection = window.getSelection();
            var range = document.createRange();
            range.selectNodeContents(event.target);
            selection === null || selection === void 0 ? void 0 : selection.removeAllRanges();
            selection === null || selection === void 0 ? void 0 : selection.addRange(range);
        };
        return _this;
    }
    ErrorMessageComponent.prototype.getErrorDetailsMessage = function () {
        return this.state.showDetails ? preact_2.div({
            className: 'detailedErrorMessage',
            onClick: this.selectDetailMessage,
        }, this.props.detailedMessage) : [];
    };
    ErrorMessageComponent.prototype.render = function () {
        var props = this.props;
        return preact_2.div({ className: preact_2.classNames('errorMessage', { hasDetailsSection: !!this.props.detailedMessage }) }, preact_2.div({ className: 'errorMessageMain', dangerouslySetInnerHTML: { __html: props.messageHtml.html } }), this.props.detailedMessage ? preact_2.div({
            className: 'detailedErrorSection'
        }, preact_2.button({ className: 'detailsButton', onClick: this.toggleErrorDetails }, localization_1.getTranslation().serverSelector.button.details), this.getErrorDetailsMessage()) : []);
    };
    return ErrorMessageComponent;
}(preact_1.Component));
exports.errorMessageComponent = preact_2.createPreactFactory(ErrorMessageComponent);

},{"../localization":14,"../utils/preact":23,"preact":2,"tslib":3}],9:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.externalTextLink = void 0;
var tslib_1 = require("tslib");
var preact_1 = require("preact");
var preact_2 = require("../utils/preact");
var ExternalTextLink = (function (_super) {
    tslib_1.__extends(ExternalTextLink, _super);
    function ExternalTextLink() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    ExternalTextLink.prototype.render = function () {
        var props = this.props;
        return preact_2.a({
            className: 'externalTextLink',
            onClick: function (event) {
                event.preventDefault();
                props.openWindow(props.url);
            }, href: '#'
        }, this.props.text);
    };
    return ExternalTextLink;
}(preact_1.Component));
exports.externalTextLink = preact_2.createPreactFactory(ExternalTextLink);

},{"../utils/preact":23,"preact":2,"tslib":3}],10:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.helpLink = exports.HELP_LINK_URLS = exports.getLocalizedDefaultHelpLink = void 0;
var tslib_1 = require("tslib");
var preact_1 = require("preact");
var preact_2 = require("../utils/preact");
var localization_1 = require("../localization");
function getLocalizedDefaultHelpLink() {
    return (localization_1.getLocale() === 'de') ? exports.HELP_LINK_URLS.de : exports.HELP_LINK_URLS.en;
}
exports.getLocalizedDefaultHelpLink = getLocalizedDefaultHelpLink;
exports.HELP_LINK_URLS = {
    en: 'https://docs.acrolinx.com/doc/en',
    de: 'https://docs.acrolinx.com/doc/en'
};
var HelpLink = (function (_super) {
    tslib_1.__extends(HelpLink, _super);
    function HelpLink() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    HelpLink.prototype.render = function () {
        var props = this.props;
        return preact_2.a({
            className: 'icon-help',
            title: localization_1.getTranslation().serverSelector.tooltip.openHelp,
            onClick: function (event) {
                event.preventDefault();
                event.stopPropagation();
                props.openWindow(props.initParameters.helpUrl || getLocalizedDefaultHelpLink());
            }, href: '#'
        });
    };
    return HelpLink;
}(preact_1.Component));
exports.helpLink = preact_2.createPreactFactory(HelpLink);

},{"../localization":14,"../utils/preact":23,"preact":2,"tslib":3}],11:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.focusAddressInputField = exports.severSelectorFormComponent = exports.CANT_CONNECT_HELP_LINK_URLS = exports.getLocalizedCantConnectHelpLink = void 0;
var tslib_1 = require("tslib");
var preact_1 = require("preact");
var preact_2 = require("../utils/preact");
var localization_1 = require("../localization");
var utils_1 = require("../utils/utils");
var external_text_link_1 = require("./external-text-link");
var error_message_1 = require("./error-message");
var help_link_1 = require("./help-link");
var SERVER_ADDRESS_INPUT_FIELD_CLASS = 'serverAddress';
function getLocalizedCantConnectHelpLink() {
    return (localization_1.getLocale() === 'de') ? exports.CANT_CONNECT_HELP_LINK_URLS.de : exports.CANT_CONNECT_HELP_LINK_URLS.en;
}
exports.getLocalizedCantConnectHelpLink = getLocalizedCantConnectHelpLink;
exports.CANT_CONNECT_HELP_LINK_URLS = {
    en: 'https://docs.acrolinx.com/coreplatform/latest/en/the-sidebar/connect-your-sidebar-to-acrolinx',
    de: 'https://docs.acrolinx.com/coreplatform/latest/de/the-sidebar/connect-your-sidebar-to-acrolinx'
};
var SeverSelectorFormComponent = (function (_super) {
    tslib_1.__extends(SeverSelectorFormComponent, _super);
    function SeverSelectorFormComponent() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.onSubmit = function (event) {
            event.preventDefault();
            _this.props.onSubmit(_this.serverAddressField.value);
        };
        return _this;
    }
    SeverSelectorFormComponent.prototype.render = function () {
        var _this = this;
        var t = localization_1.getTranslation().serverSelector;
        var props = this.props;
        var httpsRequired = utils_1.isHttpsRequired({ enforceHTTPS: props.enforceHTTPS, windowLocation: window.location });
        return preact_2.form({ className: 'serverSelectorFormComponent', onSubmit: this.onSubmit }, preact_2.div({
            className: 'logoHeader'
        }, help_link_1.helpLink(props)), preact_2.div({ className: 'formContent' }, preact_2.div({ className: 'paddedFormContent' }, preact_2.h1({
            className: 'serverAddressTitle',
            title: httpsRequired ? t.tooltip.httpsRequired : ''
        }, t.title.serverAddress, httpsRequired ? preact_2.span({ className: 'httpsRequiredIcon' }) : []), preact_2.input({
            className: SERVER_ADDRESS_INPUT_FIELD_CLASS,
            name: 'acrolinxServerAddress',
            placeholder: t.placeHolder.serverAddress, autofocus: true,
            ref: function (inputEl) {
                _this.serverAddressField = inputEl;
            },
            defaultValue: props.serverAddress,
            spellCheck: "false"
        }), preact_2.div({ className: 'buttonGroup' }, external_text_link_1.externalTextLink({
            url: getLocalizedCantConnectHelpLink(),
            openWindow: props.openWindow,
            text: t.links.cantConnect
        }), preact_2.button({
            type: 'submit',
            className: "submitButton",
            disabled: props.isConnectButtonDisabled
        }, t.button.connect)), preact_2.a({
            onClick: function (event) {
                event.preventDefault();
                props.onAboutLink();
            },
            href: '#'
        }, t.links.about)), props.errorMessage ? error_message_1.errorMessageComponent(props.errorMessage) : []));
    };
    return SeverSelectorFormComponent;
}(preact_1.Component));
exports.severSelectorFormComponent = preact_2.createPreactFactory(SeverSelectorFormComponent);
function focusAddressInputField(el) {
    var addressFieldElement = el.getElementsByClassName(SERVER_ADDRESS_INPUT_FIELD_CLASS).item(0);
    if (addressFieldElement) {
        addressFieldElement.focus();
    }
}
exports.focusAddressInputField = focusAddressInputField;

},{"../localization":14,"../utils/preact":23,"../utils/utils":25,"./error-message":8,"./external-text-link":9,"./help-link":10,"preact":2,"tslib":3}],12:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.FORCE_MESSAGE_ADAPTER = exports.FORCE_SIDEBAR_URL = exports.REQUEST_INIT_TIMEOUT_MS = exports.URL_PREFIXES_NEEDING_MESSAGE_ADAPTER = exports.SERVER_SELECTOR_VERSION = void 0;
exports.SERVER_SELECTOR_VERSION = '3.1.1.191';
exports.URL_PREFIXES_NEEDING_MESSAGE_ADAPTER = ['chrome-extension://', 'moz-extension://', 'resource://', 'ms-browser-extension://', 'safari-web-extension://'];
exports.REQUEST_INIT_TIMEOUT_MS = 60 * 1000;
exports.FORCE_SIDEBAR_URL = '';
exports.FORCE_MESSAGE_ADAPTER = false;

},{}],13:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.sanitizeClientComponent = exports.getClientComponentFallbackId = exports.extendClientComponents = exports.hackInitParameters = void 0;
var tslib_1 = require("tslib");
var sidebar_interface_1 = require("@acrolinx/sidebar-interface");
var utils_1 = require("./acrolinx-sidebar-integration/utils/utils");
var constants_1 = require("./constants");
var localization_1 = require("./localization");
var logging_1 = require("./utils/logging");
function hackInitParameters(initParameters, serverAddress) {
    var ignoreAccessToken = initParameters.accessToken && initParameters.showServerSelector;
    if (ignoreAccessToken) {
        logging_1.warn("Ignore accessToken because showServerSelector=" + initParameters.showServerSelector);
    }
    return tslib_1.__assign(tslib_1.__assign({}, initParameters), { serverAddress: serverAddress, accessToken: ignoreAccessToken ? undefined : initParameters.accessToken, showServerSelector: false, corsWithCredentials: initParameters.corsWithCredentials || utils_1.isCorsWithCredentialsNeeded(serverAddress), supported: tslib_1.__assign(tslib_1.__assign({}, initParameters.supported), { showServerSelector: initParameters.showServerSelector }), clientComponents: extendClientComponents(initParameters.clientComponents) });
}
exports.hackInitParameters = hackInitParameters;
function extendClientComponents(clientComponents) {
    return (clientComponents || []).concat({
        id: 'com.acrolinx.serverselector',
        name: localization_1.getTranslation().serverSelector.aboutItems.serverSelector,
        version: constants_1.SERVER_SELECTOR_VERSION,
        category: sidebar_interface_1.SoftwareComponentCategory.DEFAULT
    });
}
exports.extendClientComponents = extendClientComponents;
function getClientComponentFallbackId(name, index) {
    var idFromName = (name || '').replace(/[^a-zA-Z0-9]+/g, '.');
    return (idFromName === '.' || idFromName === '')
        ? 'unknown.client.component.id.with.index.' + index
        : idFromName;
}
exports.getClientComponentFallbackId = getClientComponentFallbackId;
function sanitizeClientComponent(clientComponent, index) {
    return tslib_1.__assign(tslib_1.__assign({}, clientComponent), { id: clientComponent.id || getClientComponentFallbackId(clientComponent.name, index), name: clientComponent.name || clientComponent.id || 'unknown client component name', version: clientComponent.version || '0.0.0.0' });
}
exports.sanitizeClientComponent = sanitizeClientComponent;

},{"./acrolinx-sidebar-integration/utils/utils":6,"./constants":12,"./localization":14,"./utils/logging":22,"@acrolinx/sidebar-interface":1,"tslib":3}],14:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getLocale = exports.getTranslation = exports.setLanguage = void 0;
var translations_1 = require("../tmp/generated/translations");
var currentTranslation = translations_1.en;
var currentLocale = 'en';
function setLanguage(languageCode) {
    currentLocale = sanitizeClientLocale(languageCode);
    currentTranslation = translations_1.translations[currentLocale] || translations_1.en;
}
exports.setLanguage = setLanguage;
function getTranslation() {
    return currentTranslation;
}
exports.getTranslation = getTranslation;
function getLocale() {
    return currentLocale;
}
exports.getLocale = getLocale;
function sanitizeClientLocale(languageCode) {
    return (languageCode || 'en').toLowerCase().slice(0, 2).replace('jp', 'ja');
}

},{"../tmp/generated/translations":27}],15:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.startMainController = void 0;
var logging_1 = require("./utils/logging");
var utils_1 = require("./utils/utils");
var sidebar_loader_1 = require("./acrolinx-sidebar-integration/utils/sidebar-loader");
var proxy_acrolinx_plugin_1 = require("./proxies/proxy-acrolinx-plugin");
var constants_1 = require("./constants");
var message_adapter_1 = require("./acrolinx-sidebar-integration/message-adapter/message-adapter");
var proxy_acrolinx_sidebar_1 = require("./proxies/proxy-acrolinx-sidebar");
var localization_1 = require("./localization");
var validation_1 = require("./utils/validation");
var preact_1 = require("preact");
var about_1 = require("./components/about");
var init_parameters_1 = require("./init-parameters");
var server_selector_form_1 = require("./components/server-selector-form");
var error_message_1 = require("./components/error-message");
var acrolinx_storage_1 = require("./utils/acrolinx-storage");
var debug_1 = require("./utils/debug");
var logging = require("./utils/logging");
var SERVER_ADDRESS_KEY = 'acrolinx.serverSelector.serverAddress';
var PageId;
(function (PageId) {
    PageId["ABOUT"] = "aboutPage";
    PageId["SERVER_SELECTOR"] = "serverSelectorFormPage";
    PageId["ERROR_MESSAGE"] = "errorMessagePage";
    PageId["LOADING_SIDEBAR_MESSAGE"] = "loadingSidebarMessagePage";
    PageId["SIDEBAR_CONTAINER"] = "sidebarContainer";
})(PageId || (PageId = {}));
var TEMPLATE = "\n  <div id=\"" + PageId.SERVER_SELECTOR + "\" style=\"display: none\"></div>\n  <div id=\"" + PageId.ABOUT + "\" style=\"display: none\"></div>\n  <div id=\"" + PageId.ERROR_MESSAGE + "\" style=\"display: none\"></div>\n  <div id=\"" + PageId.LOADING_SIDEBAR_MESSAGE + "\">\n    <div class=\"loader loaderJsLoaded\"><span class=\"fallbackLoadingMessage\">Loading ...</span></div>\n  </div>\n  <div id=\"" + PageId.SIDEBAR_CONTAINER + "\" style=\"display: none\"></div>\n";
function isMessageAdapterNeeded() {
    return constants_1.FORCE_MESSAGE_ADAPTER || utils_1.startsWithAnyOf(window.location.href, constants_1.URL_PREFIXES_NEEDING_MESSAGE_ADAPTER);
}
var SidebarState;
(function (SidebarState) {
    SidebarState[SidebarState["BEFORE_REQUEST_INIT"] = 0] = "BEFORE_REQUEST_INIT";
    SidebarState[SidebarState["AFTER_REQUEST_INIT"] = 1] = "AFTER_REQUEST_INIT";
})(SidebarState || (SidebarState = {}));
function startMainController(opts) {
    if (opts === void 0) { opts = {}; }
    logging_1.setExternalLog(function (logEntry) {
        var acrolinxPlug = window.acrolinxPlugin;
        if (acrolinxPlug === null || acrolinxPlug === void 0 ? void 0 : acrolinxPlug.log) {
            acrolinxPlug.log(logEntry);
        }
    });
    logging.log('Loading acrolinx sidebar startpage ' + constants_1.SERVER_SELECTOR_VERSION);
    debug_1.initDebug();
    var windowAny = window;
    var sidebarProxy = new proxy_acrolinx_sidebar_1.ProxyAcrolinxSidebar(onInitFromPlugin);
    var acrolinxPlugin;
    var initParametersFromPlugin;
    windowAny.acrolinxSidebar = sidebarProxy;
    var useMessageAdapter = isMessageAdapterNeeded();
    var appElement = utils_1.$('#app');
    appElement.innerHTML = TEMPLATE;
    var sidebarContainer = utils_1.$byId(PageId.SIDEBAR_CONTAINER);
    var errorMessageEl = utils_1.$byId(PageId.ERROR_MESSAGE);
    var aboutPage = utils_1.$byId(PageId.ABOUT);
    var serverSelectorFormPage = utils_1.$byId(PageId.SERVER_SELECTOR);
    var sidebarIFrameElement;
    var serverAddress;
    var selectedPage;
    var requestInitTimeoutWatcher = new utils_1.TimeoutWatcher(onRequestInitTimeout, opts.requestInitTimeOutMs || constants_1.REQUEST_INIT_TIMEOUT_MS);
    var sidebarState = SidebarState.BEFORE_REQUEST_INIT;
    showPage(PageId.LOADING_SIDEBAR_MESSAGE);
    proxy_acrolinx_plugin_1.waitForAcrolinxPlugin(function (acrolinxPluginArg) {
        acrolinx_storage_1.initAcrolinxStorage();
        serverAddress = acrolinx_storage_1.getAcrolinxSimpleStorage().getItem(SERVER_ADDRESS_KEY);
        acrolinxPlugin = acrolinxPluginArg;
        acrolinxPlugin.requestInit();
        if (useMessageAdapter) {
            logging.log('useMessageAdapter');
            addEventListener('message', onMessageFromSidebar, false);
        }
    });
    function openLogFile() {
        logging.log('clicked openLogFile');
        if (acrolinxPlugin.openLogFile) {
            acrolinxPlugin.openLogFile();
        }
        else {
            logging.error('acrolinxPlugin.openLogFile is not defined!');
        }
    }
    function openWindow(options) {
        if (acrolinxPlugin && acrolinxPlugin.openWindow &&
            !(initParametersFromPlugin && initParametersFromPlugin.openWindowDirectly)) {
            acrolinxPlugin.openWindow(options);
        }
        else {
            window.open(options.url);
        }
    }
    function onSubmit(serverAddressInput) {
        logging.log("User tries to connect with server \"" + serverAddressInput + "\"");
        var newServerAddressResult = validation_1.sanitizeAndValidateServerAddress(serverAddressInput, {
            enforceHTTPS: initParametersFromPlugin.enforceHTTPS,
            windowLocation: window.location
        });
        newServerAddressResult.match({
            ok: function (newServerAddress) {
                serverAddress = newServerAddress;
                tryToLoadSidebar(serverAddress);
            },
            err: function (errorMessage) {
                showServerSelector({ errorMessage: simpleErrorMessage(errorMessage) });
            }
        });
    }
    function tryToLoadSidebar(acrolinxServerAddress) {
        logging.log("Try to load sidebar from \"" + acrolinxServerAddress + "\"");
        sidebarContainer.innerHTML = '';
        sidebarIFrameElement = document.createElement('iframe');
        sidebarContainer.appendChild(sidebarIFrameElement);
        var minimumSidebarVersion = utils_1.parseVersionNumberWithFallback(initParametersFromPlugin.minimumSidebarVersion);
        var sidebarUrl;
        if (minimumSidebarVersion.length !== 0 && minimumSidebarVersion[0] === 15) {
            sidebarUrl = utils_1.combinePathParts(acrolinxServerAddress, '/sidebar/v15/');
        }
        else {
            sidebarUrl = utils_1.combinePathParts(acrolinxServerAddress, '/sidebar/v14/');
        }
        var loadSidebarProps = {
            sidebarUrl: sidebarUrl,
            useMessageAdapter: useMessageAdapter,
            timeoutWatcher: requestInitTimeoutWatcher,
            minimumSidebarVersion: minimumSidebarVersion
        };
        if (selectedPage === PageId.SERVER_SELECTOR) {
            renderServerSelectorForm({ isConnectButtonDisabled: true });
        }
        else {
            addSidebarLoadingStyles();
            showPage(PageId.LOADING_SIDEBAR_MESSAGE);
        }
        sidebar_loader_1.loadSidebarIntoIFrame(loadSidebarProps, sidebarIFrameElement, function (error) {
            if (error) {
                renderServerSelectorForm({ isConnectButtonDisabled: false });
                onSidebarLoadError(acrolinxServerAddress, error);
                return;
            }
            if (initParametersFromPlugin.showServerSelector) {
                acrolinx_storage_1.getAcrolinxSimpleStorage().setItem(SERVER_ADDRESS_KEY, acrolinxServerAddress);
            }
            showPage(PageId.SIDEBAR_CONTAINER);
            if (useMessageAdapter) {
                return;
            }
            var contentWindowAny = sidebarIFrameElement.contentWindow;
            acrolinx_storage_1.injectAcrolinxStorageIntoSidebarIfAvailable(window, contentWindowAny);
            contentWindowAny.acrolinxPlugin = new proxy_acrolinx_plugin_1.ProxyAcrolinxPlugin({
                requestInitListener: function () {
                    sidebarProxy.acrolinxSidebar = contentWindowAny.acrolinxSidebar;
                    onRequestInit();
                },
                acrolinxPlugin: acrolinxPlugin,
                serverAddress: acrolinxServerAddress,
                showServerSelector: showServerSelector,
                openWindow: openWindow
            });
        });
    }
    function addSidebarLoadingStyles() {
        var loader = appElement.querySelector('.loader');
        if (loader) {
            loader.classList.add('loadSidebarHtml');
        }
    }
    function simpleErrorMessage(messageHtml, details) {
        return {
            messageHtml: { html: messageHtml },
            detailedMessage: details && JSON.stringify(details)
        };
    }
    function getSidebarLoadErrorMessage(currentServerAddress, error) {
        var errorMessages = localization_1.getTranslation().serverSelector.message;
        switch (error.acrolinxErrorCode) {
            case 'httpErrorStatus':
            case 'noSidebar':
                return simpleErrorMessage(errorMessages.serverIsNoAcrolinxServerOrHasNoSidebar, error);
            case 'sidebarVersionIsBelowMinimum':
                return simpleErrorMessage(errorMessages.outdatedServer, error);
            default:
                var errorMessage = utils_1.isHttpUrl(currentServerAddress)
                    ? errorMessages.serverConnectionProblemHttp
                    : errorMessages.serverConnectionProblemHttps;
                return simpleErrorMessage(errorMessage, error);
        }
    }
    function onSidebarLoadError(currentServerAddress, error) {
        showSidebarLoadError(getSidebarLoadErrorMessage(currentServerAddress, error));
    }
    function showSidebarLoadError(errorMessage) {
        if (initParametersFromPlugin.showServerSelector) {
            showServerSelector({ errorMessage: errorMessage });
        }
        else {
            showErrorMessagePage(errorMessage);
        }
    }
    function showPage(page) {
        var divs = appElement.childNodes;
        for (var i = 0; i < divs.length; ++i) {
            var div = divs[i];
            utils_1.setDisplayed(div, page === div.id);
        }
        selectedPage = page;
    }
    function showServerSelector(props) {
        if (props === void 0) { props = {}; }
        sidebarState = SidebarState.BEFORE_REQUEST_INIT;
        utils_1.cleanIFrameContainerIfNeeded(sidebarContainer, function () {
            showPage(PageId.SERVER_SELECTOR);
            server_selector_form_1.focusAddressInputField(serverSelectorFormPage);
            renderServerSelectorForm(props);
        });
    }
    function renderServerSelectorForm(props) {
        if (props === void 0) { props = {}; }
        preact_1.render(server_selector_form_1.severSelectorFormComponent({
            onSubmit: onSubmit,
            onAboutLink: onAboutLink,
            serverAddress: serverAddress,
            enforceHTTPS: initParametersFromPlugin.enforceHTTPS,
            isConnectButtonDisabled: props.isConnectButtonDisabled,
            openWindow: function (url) { return openWindow({ url: url }); },
            errorMessage: props.errorMessage,
            initParameters: initParametersFromPlugin
        }), serverSelectorFormPage, serverSelectorFormPage.firstChild);
    }
    function showErrorMessagePage(errorMessageProps) {
        showPage(PageId.ERROR_MESSAGE);
        preact_1.render(error_message_1.errorMessageComponent(errorMessageProps), errorMessageEl, errorMessageEl.firstChild);
    }
    function onInitFromPlugin(initParameters) {
        var _a;
        initParametersFromPlugin = initParameters;
        localization_1.setLanguage(initParameters.clientLocale);
        if (((_a = initParameters.supported) === null || _a === void 0 ? void 0 : _a.log) && acrolinxPlugin.log) {
            logging_1.setExternalLog(function (logEntry) { return acrolinxPlugin.log(logEntry); });
        }
        else {
            logging_1.setExternalLog(undefined);
        }
        if (initParameters.showServerSelector) {
            if (serverAddress) {
                tryToLoadSidebar(serverAddress);
            }
            else {
                if (initParameters.serverAddress) {
                    serverAddress = initParameters.serverAddress;
                }
                showServerSelector();
            }
        }
        else {
            logging.log('Load directly!');
            serverAddress = initParameters.serverAddress || utils_1.getDefaultServerAddress(window.location);
            tryToLoadSidebar(serverAddress);
        }
    }
    function onMessageFromSidebar(messageEvent) {
        if (!sidebarIFrameElement || !sidebarIFrameElement.contentWindow ||
            messageEvent.source !== sidebarIFrameElement.contentWindow) {
            return;
        }
        var command = messageEvent.data.command;
        var args = messageEvent.data.args;
        if (command === 'setStorageItem') {
            logging.log('onMessageFromSidebar', command, args[0]);
        }
        else {
            logging.log('onMessageFromSidebar', messageEvent, command, args);
        }
        switch (command) {
            case 'requestInit':
                sidebarProxy.acrolinxSidebar = message_adapter_1.createSidebarMessageProxy(sidebarIFrameElement.contentWindow);
                onRequestInit();
                break;
            case 'showServerSelector':
                showServerSelector();
                break;
            case 'setStorageItem':
                acrolinx_storage_1.getAcrolinxSimpleStorage().setItem(args[0], args[1]);
                break;
            case 'removeStorageItem':
                acrolinx_storage_1.getAcrolinxSimpleStorage().removeItem(args[0]);
                break;
            default:
                var acrolinxPluginAny = windowAny.acrolinxPlugin;
                var commandFunction = acrolinxPluginAny[command];
                if (commandFunction) {
                    commandFunction.apply(acrolinxPluginAny, args);
                }
                else {
                    logging.error("Plugin does not support command \"" + command + "\"", args);
                }
        }
    }
    function onRequestInit() {
        logging.log("Sidebar is loaded completely and has requested init (\"" + serverAddress + "\")");
        sidebarState = SidebarState.AFTER_REQUEST_INIT;
        requestInitTimeoutWatcher.stop();
        function initSidebar() {
            sidebarProxy.acrolinxSidebar.init(init_parameters_1.hackInitParameters(initParametersFromPlugin, serverAddress));
        }
        var acrolinxSimpleStorage = acrolinx_storage_1.getAcrolinxSimpleStorage();
        if (acrolinxSimpleStorage.getItems) {
            acrolinxSimpleStorage.getItems(function (data) {
                sidebarProxy.acrolinxSidebar.setStorage({ data: data });
                initSidebar();
            });
        }
        else {
            initSidebar();
        }
    }
    function onRequestInitTimeout() {
        if (sidebarState === SidebarState.BEFORE_REQUEST_INIT) {
            logging.error("Sidebar took too long to load from \"" + serverAddress + "\"");
            showSidebarLoadError({ messageHtml: { html: localization_1.getTranslation().serverSelector.message.loadSidebarTimeout } });
        }
    }
    function onAboutLink() {
        showPage(PageId.ABOUT);
        preact_1.render(about_1.aboutComponent({
            onBack: function () {
                showServerSelector();
            },
            logFileLocation: initParametersFromPlugin.logFileLocation,
            openLogFile: openLogFile,
            clientComponents: init_parameters_1.extendClientComponents(initParametersFromPlugin.clientComponents),
            openWindow: function (url) { return openWindow({ url: url }); },
            initParameters: initParametersFromPlugin
        }), aboutPage, aboutPage.firstChild);
    }
}
exports.startMainController = startMainController;

},{"./acrolinx-sidebar-integration/message-adapter/message-adapter":4,"./acrolinx-sidebar-integration/utils/sidebar-loader":5,"./components/about":7,"./components/error-message":8,"./components/server-selector-form":11,"./constants":12,"./init-parameters":13,"./localization":14,"./proxies/proxy-acrolinx-plugin":17,"./proxies/proxy-acrolinx-sidebar":18,"./utils/acrolinx-storage":19,"./utils/debug":20,"./utils/logging":22,"./utils/utils":25,"./utils/validation":26,"preact":2}],16:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var main_controller_1 = require("./main-controller");
setTimeout(main_controller_1.startMainController, 500);

},{"./main-controller":15}],17:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.waitForAcrolinxPlugin = exports.ProxyAcrolinxPlugin = exports.POLL_FOR_PLUGIN_INTERVAL_MS = void 0;
var logging = require("../utils/logging");
exports.POLL_FOR_PLUGIN_INTERVAL_MS = 10;
var ProxyAcrolinxPlugin = (function () {
    function ProxyAcrolinxPlugin(props) {
        this.props = props;
    }
    ProxyAcrolinxPlugin.prototype.requestInit = function () {
        this.props.requestInitListener();
    };
    ProxyAcrolinxPlugin.prototype.onInitFinished = function (initFinishedResult) {
        this.props.acrolinxPlugin.onInitFinished(initFinishedResult);
    };
    ProxyAcrolinxPlugin.prototype.requestGlobalCheck = function (options) {
        if (options) {
            this.props.acrolinxPlugin.requestGlobalCheck(options);
        }
        else {
            this.props.acrolinxPlugin.requestGlobalCheck();
        }
    };
    ProxyAcrolinxPlugin.prototype.onCheckResult = function (checkResult) {
        this.props.acrolinxPlugin.onCheckResult(checkResult);
    };
    ProxyAcrolinxPlugin.prototype.selectRanges = function (checkId, matches) {
        this.props.acrolinxPlugin.selectRanges(checkId, matches);
    };
    ProxyAcrolinxPlugin.prototype.replaceRanges = function (checkId, matchesWithReplacement) {
        this.props.acrolinxPlugin.replaceRanges(checkId, matchesWithReplacement);
    };
    ProxyAcrolinxPlugin.prototype.openWindow = function (opts) {
        this.props.openWindow(opts);
    };
    ProxyAcrolinxPlugin.prototype.showServerSelector = function () {
        this.props.showServerSelector();
    };
    ProxyAcrolinxPlugin.prototype.openLogFile = function () {
        if (this.props.acrolinxPlugin.openLogFile) {
            this.props.acrolinxPlugin.openLogFile();
        }
        else {
            logging.error('openLogFile is not supported');
        }
    };
    ProxyAcrolinxPlugin.prototype.requestCheckForDocumentInBatch = function (documentIdentifier) {
        if (this.props.acrolinxPlugin.requestCheckForDocumentInBatch) {
            this.props.acrolinxPlugin.requestCheckForDocumentInBatch(documentIdentifier);
        }
        else {
            logging.error('requestCheckForDocumentInBatch is not supported');
        }
    };
    ProxyAcrolinxPlugin.prototype.openDocumentInEditor = function (documentIdentifier) {
        if (this.props.acrolinxPlugin.openDocumentInEditor) {
            return this.props.acrolinxPlugin.openDocumentInEditor(documentIdentifier);
        }
        else {
            logging.error('openDocumentInEditor is not supported');
        }
    };
    ProxyAcrolinxPlugin.prototype.log = function (logEntry) {
        if (this.props.acrolinxPlugin.log) {
            this.props.acrolinxPlugin.log(logEntry);
        }
        else {
            logging.error('log is not supported', logEntry);
        }
    };
    return ProxyAcrolinxPlugin;
}());
exports.ProxyAcrolinxPlugin = ProxyAcrolinxPlugin;
function waitForAcrolinxPlugin(callback) {
    var windowAny = window;
    if (windowAny.acrolinxPlugin) {
        callback(windowAny.acrolinxPlugin);
    }
    else {
        setTimeout(function () {
            waitForAcrolinxPlugin(callback);
        }, exports.POLL_FOR_PLUGIN_INTERVAL_MS);
    }
}
exports.waitForAcrolinxPlugin = waitForAcrolinxPlugin;

},{"../utils/logging":22}],18:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.ProxyAcrolinxSidebar = void 0;
var logging = require("../utils/logging");
var ProxyAcrolinxSidebar = (function () {
    function ProxyAcrolinxSidebar(initListener) {
        this.initListener = initListener;
        this.configureQueue = [];
    }
    Object.defineProperty(ProxyAcrolinxSidebar.prototype, "serverAddress", {
        get: function () {
            return this._serverAddress;
        },
        set: function (value) {
            this._serverAddress = value;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(ProxyAcrolinxSidebar.prototype, "acrolinxSidebar", {
        get: function () {
            return this._acrolinxSidebar;
        },
        set: function (sidebar) {
            this._acrolinxSidebar = sidebar;
            while (this.configureQueue.length > 0) {
                this._acrolinxSidebar.configure(this.configureQueue.splice(0, 1)[0]);
            }
        },
        enumerable: false,
        configurable: true
    });
    ProxyAcrolinxSidebar.prototype.init = function (initParameters) {
        this.initListener(initParameters);
    };
    ProxyAcrolinxSidebar.prototype.configure = function (sidebarConfiguration) {
        if (this.acrolinxSidebar) {
            this.acrolinxSidebar.configure(sidebarConfiguration);
        }
        else {
            this.configureQueue.push(sidebarConfiguration);
        }
    };
    ProxyAcrolinxSidebar.prototype.checkGlobal = function (documentContent, options) {
        return this.acrolinxSidebar.checkGlobal(documentContent, options);
    };
    ProxyAcrolinxSidebar.prototype.onGlobalCheckRejected = function () {
        this.acrolinxSidebar.onGlobalCheckRejected();
    };
    ProxyAcrolinxSidebar.prototype.invalidateRanges = function (invalidCheckedDocumentRanges) {
        this.acrolinxSidebar.invalidateRanges(invalidCheckedDocumentRanges);
    };
    ProxyAcrolinxSidebar.prototype.onVisibleRangesChanged = function (checkedDocumentRanges) {
        return this.acrolinxSidebar.onVisibleRangesChanged(checkedDocumentRanges);
    };
    ProxyAcrolinxSidebar.prototype.showMessage = function (message) {
        return this.acrolinxSidebar.showMessage(message);
    };
    ProxyAcrolinxSidebar.prototype.initBatchCheck = function (documentIdentifiers) {
        if (this.acrolinxSidebar.initBatchCheck) {
            return this.acrolinxSidebar.initBatchCheck(documentIdentifiers);
        }
        else {
            logging.error('initBatchCheck is not supported');
        }
    };
    ProxyAcrolinxSidebar.prototype.checkDocumentInBatch = function (documentIdentifier, documentContent, options) {
        if (this.acrolinxSidebar.checkDocumentInBatch) {
            return this.acrolinxSidebar.checkDocumentInBatch(documentIdentifier, documentContent, options);
        }
        else {
            logging.error('checkDocumentInBatch is not supported');
        }
    };
    return ProxyAcrolinxSidebar;
}());
exports.ProxyAcrolinxSidebar = ProxyAcrolinxSidebar;

},{"../utils/logging":22}],19:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.forTesting = exports.injectAcrolinxStorageIntoSidebarIfAvailable = exports.initAcrolinxStorage = exports.getAcrolinxSimpleStorage = exports.AcrolinxSimpleStorageInMemory = void 0;
var logging = require("./logging");
function isLocalStorageAvailable(storage) {
    try {
        if (!storage) {
            return false;
        }
        var x = '__storage_test__';
        storage.setItem(x, x);
        if (storage.getItem(x) !== x) {
            return false;
        }
        storage.removeItem(x);
        return true;
    }
    catch (e) {
        return false;
    }
}
var AcrolinxSimpleStorageInMemory = (function () {
    function AcrolinxSimpleStorageInMemory() {
        this.dataMap = {};
    }
    AcrolinxSimpleStorageInMemory.prototype.clear = function () {
        this.dataMap = {};
    };
    AcrolinxSimpleStorageInMemory.prototype.setItem = function (key, data) {
        this.dataMap[key] = data;
    };
    AcrolinxSimpleStorageInMemory.prototype.getItem = function (key) {
        var value = this.dataMap[key];
        return value === undefined ? null : value;
    };
    AcrolinxSimpleStorageInMemory.prototype.removeItem = function (key) {
        delete this.dataMap[key];
    };
    return AcrolinxSimpleStorageInMemory;
}());
exports.AcrolinxSimpleStorageInMemory = AcrolinxSimpleStorageInMemory;
var disableLocalStorageForTesting = false;
function getAcrolinxSimpleStorageAtInitInternal(acrolinxStorageArg, localStorageArg) {
    if (acrolinxStorageArg) {
        logging.log('acrolinxStartpage: Using window.acrolinxStorage');
        return acrolinxStorageArg;
    }
    else if (isLocalStorageAvailable(localStorageArg) && !disableLocalStorageForTesting) {
        logging.log('acrolinxStartpage: Using window.localStorage');
        return localStorageArg;
    }
    else {
        logging.log('acrolinxStartpage: Using AcrolinxSimpleStorageInMemory');
        return new AcrolinxSimpleStorageInMemory();
    }
}
function getLocalStorageSafe() {
    try {
        return window.localStorage;
    }
    catch (_error) {
        return undefined;
    }
}
function getAcrolinxSimpleStorageAtInit() {
    var pimpedWindow = window;
    return getAcrolinxSimpleStorageAtInitInternal(pimpedWindow.acrolinxStorage, getLocalStorageSafe());
}
var acrolinxSimpleStorage;
function getAcrolinxSimpleStorage() {
    if (!acrolinxSimpleStorage) {
        acrolinxSimpleStorage = getAcrolinxSimpleStorageAtInit();
    }
    return acrolinxSimpleStorage;
}
exports.getAcrolinxSimpleStorage = getAcrolinxSimpleStorage;
function initAcrolinxStorage() {
    acrolinxSimpleStorage = getAcrolinxSimpleStorageAtInit();
}
exports.initAcrolinxStorage = initAcrolinxStorage;
function injectAcrolinxStorageIntoSidebarIfAvailable(currentWindow, sidebarIFrameWindow) {
    if (currentWindow.acrolinxStorage) {
        logging.log('Inject window.acrolinxStorage into sidebar');
        sidebarIFrameWindow.acrolinxStorage = currentWindow.acrolinxStorage;
    }
}
exports.injectAcrolinxStorageIntoSidebarIfAvailable = injectAcrolinxStorageIntoSidebarIfAvailable;
exports.forTesting = {
    getAcrolinxSimpleStorageAtInitInternal: getAcrolinxSimpleStorageAtInitInternal
};

},{"./logging":22}],20:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.initDebug = void 0;
var utils_1 = require("./utils");
var logging = require("./logging");
function patchFirebugUI() {
    var iFrameEl = document.getElementById('FirebugUI');
    if (!iFrameEl || !iFrameEl.contentWindow) {
        return false;
    }
    try {
        var closeButton = iFrameEl.contentWindow.document.getElementById('fbWindow_btDeactivate');
        closeButton.style.display = 'none';
    }
    catch (_error) {
        return false;
    }
    return true;
}
function waitToPatchFirebugUI() {
    if (!patchFirebugUI()) {
        setTimeout(waitToPatchFirebugUI, 500);
    }
}
function loadFirebugLite() {
    logging.log('Loading firebug lite into sidebar startpage ...');
    utils_1.loadScript('https://getfirebug.com/releases/lite/1.3/firebug-lite.js#startOpened=true');
    waitToPatchFirebugUI();
}
function waitForFirebugCheatCode() {
    var cheatCode = '';
    var isMouseDown = false;
    document.addEventListener('keypress', function (keyEvent) {
        if (isMouseDown) {
            var key = keyEvent.key || String.fromCharCode(keyEvent.charCode);
            cheatCode += key;
        }
    });
    document.addEventListener('mousedown', function () {
        isMouseDown = true;
        cheatCode = '';
    });
    document.addEventListener('mouseup', function () {
        isMouseDown = false;
        if (cheatCode === 'acrofire') {
            loadFirebugLite();
        }
        else {
            if (cheatCode.length > 3) {
                logging.log('Unknown command:', cheatCode);
            }
        }
        cheatCode = '';
    });
}
function initDebug() {
    waitForFirebugCheatCode();
}
exports.initDebug = initDebug;

},{"./logging":22,"./utils":25}],21:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.forceRedrawInWebkit = exports.DEV_NULL = void 0;
function includes(s, needle) {
    return s.indexOf(needle) >= 0;
}
function isWebkit() {
    var ua = navigator.userAgent;
    return includes(ua, 'AppleWebKit') && !(includes(ua, 'Chrome') || includes(ua, 'Edge'));
}
exports.DEV_NULL = 0;
function forceRedrawInWebkit() {
    if (!isWebkit()) {
        return;
    }
    var el = document.querySelector('body');
    if (el) {
        el.style.display = 'none';
        exports.DEV_NULL = el.offsetHeight;
        el.style.display = 'block';
    }
}
exports.forceRedrawInWebkit = forceRedrawInWebkit;

},{}],22:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.setExternalLog = exports.setLoggingEnabled = exports.error = exports.warn = exports.log = void 0;
var sidebar_interface_1 = require("@acrolinx/sidebar-interface");
var LOGGING_ENABLED = true;
var externalLog;
function createLogEntry(type, args) {
    var message = args[0], details = args.slice(1);
    if (typeof message === 'string') {
        return { type: type, message: message, details: details };
    }
    else {
        return { type: type, message: 'Details', details: args };
    }
}
function log() {
    var args = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        args[_i] = arguments[_i];
    }
    if (!LOGGING_ENABLED) {
        return;
    }
    try {
        console.log.apply(console, args);
        if (externalLog) {
            externalLog(createLogEntry(sidebar_interface_1.LogEntryType.info, args));
        }
    }
    catch (e) {
    }
}
exports.log = log;
function warn() {
    var args = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        args[_i] = arguments[_i];
    }
    if (!LOGGING_ENABLED) {
        return;
    }
    try {
        console.warn.apply(console, args);
        if (externalLog) {
            externalLog(createLogEntry(sidebar_interface_1.LogEntryType.warning, args));
        }
    }
    catch (e) {
    }
}
exports.warn = warn;
function error() {
    var args = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        args[_i] = arguments[_i];
    }
    if (!LOGGING_ENABLED) {
        return;
    }
    try {
        console.error.apply(console, args);
        if (externalLog) {
            externalLog(createLogEntry(sidebar_interface_1.LogEntryType.error, args));
        }
    }
    catch (e) {
    }
}
exports.error = error;
function setLoggingEnabled(enabled) {
    LOGGING_ENABLED = enabled;
}
exports.setLoggingEnabled = setLoggingEnabled;
function setExternalLog(newExternalLog) {
    externalLog = newExternalLog;
}
exports.setExternalLog = setExternalLog;

},{"@acrolinx/sidebar-interface":1}],23:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.classNames = exports.textarea = exports.input = exports.a = exports.form = exports.p = exports.h1 = exports.button = exports.span = exports.div = exports.createPreactFactory = void 0;
var tslib_1 = require("tslib");
var preact_1 = require("preact");
function createPreactFactory(component) {
    return function (params) {
        var children = [];
        for (var _i = 1; _i < arguments.length; _i++) {
            children[_i - 1] = arguments[_i];
        }
        return preact_1.h.apply(void 0, tslib_1.__spreadArray([component, params], children));
    };
}
exports.createPreactFactory = createPreactFactory;
exports.div = createPreactFactory('div');
exports.span = createPreactFactory('span');
exports.button = createPreactFactory('button');
exports.h1 = createPreactFactory('h1');
exports.p = createPreactFactory('p');
exports.form = createPreactFactory('form');
exports.a = createPreactFactory('a');
exports.input = createPreactFactory('input');
exports.textarea = createPreactFactory('textarea');
function classNames() {
    var args = [];
    for (var _i = 0; _i < arguments.length; _i++) {
        args[_i] = arguments[_i];
    }
    var classes = [];
    for (var i = 0; i < args.length; i++) {
        var arg = args[i];
        if (!arg) {
            continue;
        }
        if ('string' === typeof arg) {
            classes.push(arg);
        }
        else if ('object' === typeof arg) {
            for (var key in arg) {
                if (arg.hasOwnProperty(key) && arg[key]) {
                    classes.push(key);
                }
            }
        }
    }
    return classes.join(' ');
}
exports.classNames = classNames;

},{"preact":2,"tslib":3}],24:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.err = exports.ok = exports.Err = exports.Ok = void 0;
var Ok = (function () {
    function Ok(value) {
        this.value = value;
    }
    Ok.prototype.match = function (matcher) {
        return matcher.ok(this.value);
    };
    return Ok;
}());
exports.Ok = Ok;
var Err = (function () {
    function Err(error) {
        this.error = error;
    }
    Err.prototype.match = function (matcher) {
        return matcher.err(this.error);
    };
    return Err;
}());
exports.Err = Err;
function ok(value) {
    return new Ok(value);
}
exports.ok = ok;
function err(error) {
    return new Err(error);
}
exports.err = err;

},{}],25:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.TimeoutWatcher = exports.loadScript = exports.cleanIFrameContainerIfNeeded = exports.parseVersionNumberWithFallback = exports.isVersionGreaterEqual = exports.sortBy = exports.getCorsOrigin = exports.validateUrl = exports.isHttpUrl = exports.isValidServerProtocol = exports.getDefaultServerAddress = exports.isHttpsRequired = exports.combinePathParts = exports.sanitizeServerAddress = exports.startsWithAnyOf = exports.startsWith = exports.setDisplayed = exports.$byId = exports.$ = void 0;
var logging = require("./logging");
function $(selector) {
    return document.querySelector(selector);
}
exports.$ = $;
function $byId(id) {
    return document.getElementById(id);
}
exports.$byId = $byId;
function setDisplayed(el, isDisplayed, display) {
    if (display === void 0) { display = 'block'; }
    if (el.style) {
        el.style.display = isDisplayed ? display : 'none';
    }
}
exports.setDisplayed = setDisplayed;
function startsWith(haystack, needle) {
    return haystack.indexOf(needle) === 0;
}
exports.startsWith = startsWith;
function startsWithAnyOf(haystack, needles) {
    return needles.some(function (needle) { return startsWith(haystack, needle); });
}
exports.startsWithAnyOf = startsWithAnyOf;
var SERVER_ADDRESS_REGEXP = new RegExp('^' +
    '(?:(?:https?)://)' +
    '(?:\\S+(?::\\S*)?@)?' +
    '(?:' +
    '(?!(?:10|127)(?:\\.\\d{1,3}){3})' +
    '(?!(?:169\\.254|192\\.168)(?:\\.\\d{1,3}){2})' +
    '(?!172\\.(?:1[6-9]|2\\d|3[0-1])(?:\\.\\d{1,3}){2})' +
    '(?:[1-9]\\d?|1\\d\\d|2[01]\\d|22[0-3])' +
    '(?:\\.(?:1?\\d{1,2}|2[0-4]\\d|25[0-5])){2}' +
    '(?:\\.(?:[1-9]\\d?|1\\d\\d|2[0-4]\\d|25[0-4]))' +
    '|' +
    '(?:(?:[a-z\\u00a1-\\uffff0-9]-*)*[a-z\\u00a1-\\uffff0-9]+)' +
    '(?:\\.(?:[a-z\\u00a1-\\uffff0-9]-*)*[a-z\\u00a1-\\uffff0-9]+)*' +
    '(?:\\.(?:[a-z\\u00a1-\\uffff]{2,}))?' +
    ')' +
    '(?::\\d{2,5})?' +
    '(?:[/?#]\\S*)?' +
    '$', 'i');
function sanitizeServerAddress(serverAddressArg, opts) {
    var trimmedAddress = serverAddressArg.trim();
    if (startsWith(trimmedAddress, '/')) {
        return getDefaultServerAddress(opts.windowLocation) + trimmedAddress;
    }
    var defaultHttpPort = 8031;
    var defaultProtocol = (includes(trimmedAddress, ':443') || isHttpsRequired(opts)) ? 'https' : 'http';
    var normalizedAddress = trimmedAddress.replace(/\/$/, '');
    var addressWithProtocol = startsWith(normalizedAddress, 'http') ? normalizedAddress : (defaultProtocol + '://' + normalizedAddress);
    var hasPortRegExp = /\/\/.+((:\d+.*)|(\/.+))$/;
    if (hasPortRegExp.test(addressWithProtocol)) {
        return addressWithProtocol;
    }
    else {
        if (startsWith(addressWithProtocol, 'http:')) {
            return addressWithProtocol + ':' + defaultHttpPort;
        }
        else {
            return addressWithProtocol;
        }
    }
}
exports.sanitizeServerAddress = sanitizeServerAddress;
function combinePathParts(part1, part2) {
    return part1.replace(/\/$/, '')
        + '/'
        + part2.replace(/^\//, '');
}
exports.combinePathParts = combinePathParts;
function isHttpsRequired(opts) {
    return opts.enforceHTTPS || opts.windowLocation.protocol === 'https:';
}
exports.isHttpsRequired = isHttpsRequired;
function getDefaultServerAddress(location) {
    if (isValidServerProtocol(location.protocol)) {
        return location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');
    }
    else {
        return '';
    }
}
exports.getDefaultServerAddress = getDefaultServerAddress;
function isValidServerProtocol(protocol) {
    return protocol === 'http:' || protocol === 'https:';
}
exports.isValidServerProtocol = isValidServerProtocol;
function isHttpUrl(url) {
    return startsWith(url, 'http:');
}
exports.isHttpUrl = isHttpUrl;
function validateUrl(url) {
    return SERVER_ADDRESS_REGEXP.test(url);
}
exports.validateUrl = validateUrl;
function includes(haystack, needle) {
    return haystack.indexOf(needle) >= 0;
}
function getCorsOrigin() {
    var location = window.location;
    return location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '');
}
exports.getCorsOrigin = getCorsOrigin;
function sortBy(array, getSortKey) {
    var cloned_array = array.slice();
    cloned_array.sort(function (a, b) {
        var sortKeyA = getSortKey(a);
        var sortKeyB = getSortKey(b);
        return sortKeyA.localeCompare(sortKeyB);
    });
    return cloned_array;
}
exports.sortBy = sortBy;
function isVersionGreaterEqual(version, minimumVersion) {
    for (var i = 0; i < version.length; i++) {
        var versionPart = version[i] || 0;
        var minimumVersionPart = minimumVersion[i] || 0;
        if (versionPart > minimumVersionPart) {
            return true;
        }
        else if (versionPart < minimumVersionPart) {
            return false;
        }
    }
    return true;
}
exports.isVersionGreaterEqual = isVersionGreaterEqual;
function parseVersionNumberWithFallback(s) {
    if (!s) {
        return [];
    }
    if (!/^(\d+.?)+$/.test(s)) {
        logging.error('Invalid version number:', s);
        return [];
    }
    try {
        return s.split('.').map(function (part) { return parseInt(part, 10); });
    }
    catch (error) {
        logging.error('Invalid version number:', s, error);
        return [];
    }
}
exports.parseVersionNumberWithFallback = parseVersionNumberWithFallback;
function cleanIFrameContainerIfNeeded(sidebarContainer, callback) {
    var iFrame = sidebarContainer.querySelector('iframe');
    if (iFrame) {
        try {
            iFrame.src = 'about:blank';
            setTimeout(function () {
                sidebarContainer.innerHTML = '';
                callback();
            }, 0);
        }
        catch (error) {
            logging.error(error);
            callback();
        }
    }
    else {
        callback();
    }
}
exports.cleanIFrameContainerIfNeeded = cleanIFrameContainerIfNeeded;
function createScriptElement(src) {
    var el = document.createElement('script');
    el.src = src;
    el.type = 'text/javascript';
    el.async = false;
    el.defer = false;
    return el;
}
function loadScript(url) {
    var head = document.querySelector('head');
    if (head) {
        head.appendChild(createScriptElement(url));
    }
    else {
        logging.error("Can not load script \"" + url + "\" because of missing head element.");
    }
}
exports.loadScript = loadScript;
var TimeoutWatcher = (function () {
    function TimeoutWatcher(onTimeout, durationMs) {
        this.onTimeout = onTimeout;
        this.durationMs = durationMs;
    }
    TimeoutWatcher.prototype.start = function () {
        var _this = this;
        if (this.timeoutId !== undefined) {
            this.stop();
            logging.warn('TimeoutWatcher: start was called twice');
        }
        this.timeoutId = setTimeout(function () {
            _this.timeoutId = undefined;
            _this.onTimeout();
        }, this.durationMs);
    };
    TimeoutWatcher.prototype.stop = function () {
        if (this.timeoutId === undefined) {
            logging.warn('TimeoutWatcher: stop was called before start');
        }
        else {
            clearTimeout(this.timeoutId);
            this.timeoutId = undefined;
        }
    };
    return TimeoutWatcher;
}());
exports.TimeoutWatcher = TimeoutWatcher;

},{"./logging":22}],26:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.sanitizeAndValidateServerAddress = void 0;
var utils_1 = require("./utils");
var result_1 = require("./result");
var localization_1 = require("../localization");
function sanitizeAndValidateServerAddress(serverAddressInput, opts) {
    var serverUrl = utils_1.sanitizeServerAddress(serverAddressInput.trim(), opts);
    if (utils_1.isHttpUrl(serverUrl) && utils_1.isHttpsRequired(opts)) {
        return new result_1.Err(localization_1.getTranslation().serverSelector.message.serverIsNotSecure);
    }
    if (!utils_1.validateUrl(serverUrl)) {
        return new result_1.Err(localization_1.getTranslation().serverSelector.message.invalidServerAddress);
    }
    return new result_1.Ok(serverUrl);
}
exports.sanitizeAndValidateServerAddress = sanitizeAndValidateServerAddress;

},{"../localization":14,"./result":24,"./utils":25}],27:[function(require,module,exports){
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.translations = exports.ja = exports.sv = exports.fr = exports.de = exports.en = exports.devTranslation = void 0;
exports.devTranslation = {
    "serverSelector": {
        "aboutItems": {
            "browserInformation": "Browser Information",
            "logFileLocation": "Log File Location",
            "serverSelector": "Start Page",
            "startPageCorsOrigin": "CORS Origin"
        },
        "button": {
            "connect": "Connect",
            "details": "Details",
            "openLogFile": "Open Log File"
        },
        "links": {
            "about": "About Acrolinx",
            "cantConnect": "Can't connect to Acrolinx?",
            "needHelp": "Help"
        },
        "message": {
            "invalidServerAddress": "It looks like the Acrolinx URL is incorrect. Check the URL and try again.",
            "loadSidebarTimeout": "It looks like the Sidebar took too long to load. Check your network connection or contact your Acrolinx administrator.",
            "outdatedServer": "It looks like your Acrolinx integration needs a newer Acrolinx Core Platform version to load the Sidebar. Ask your Acrolinx administrator to update your Core Platform.",
            "serverConnectionProblemHttp": "We couldn't connect to Acrolinx. Check that the URL is correct. If you still can't connect then contact your Acrolinx administrator. It might be that your Acrolinx Core Platform doesn’t accept insecure connections.",
            "serverConnectionProblemHttps": "We couldn't connect to Acrolinx. Check that the URL is correct. If you still can't connect then contact your Acrolinx administrator. It might be that your Acrolinx Core Platform doesn’t accept secure connections.",
            "serverIsNoAcrolinxServerOrHasNoSidebar": "It looks like this isn’t an Acrolinx URL or this Acrolinx Core Platform version doesn't support the Sidebar. Ask your Acrolinx administrator to check your Acrolinx Core Platform version if you're sure that the Acrolinx URL is correct.",
            "serverIsNotSecure": "You need to connect to Acrolinx using a secure Acrolinx URL. A secure Acrolinx URL starts with \"https\"."
        },
        "placeHolder": {
            "serverAddress": "Acrolinx URL"
        },
        "title": {
            "about": "About",
            "logFile": "Log File",
            "serverAddress": "Acrolinx URL"
        },
        "tooltip": {
            "httpsRequired": "You need to connect to Acrolinx using a secure Acrolinx URL. A secure Acrolinx URL starts with \"https\".",
            "openHelp": "Open help information in a web browser window"
        }
    }
};
exports.en = {
    "serverSelector": {
        "aboutItems": {
            "browserInformation": "Browser Information",
            "logFileLocation": "Log File Location",
            "serverSelector": "Start Page",
            "startPageCorsOrigin": "CORS Origin"
        },
        "button": {
            "connect": "Connect",
            "details": "Details",
            "openLogFile": "Open Log File"
        },
        "links": {
            "about": "About Acrolinx",
            "cantConnect": "Can't connect to Acrolinx?",
            "needHelp": "Help"
        },
        "message": {
            "invalidServerAddress": "It looks like the Acrolinx URL is incorrect. Check the URL and try again.",
            "loadSidebarTimeout": "It looks like the Sidebar took too long to load. Check your network connection or contact your Acrolinx administrator.",
            "outdatedServer": "It looks like your Acrolinx integration needs a newer Acrolinx Core Platform version to load the Sidebar. Ask your Acrolinx administrator to update your Core Platform.",
            "serverConnectionProblemHttp": "We couldn't connect to Acrolinx. Check that the URL is correct. If you still can't connect then contact your Acrolinx administrator. It might be that your Acrolinx Core Platform doesn’t accept insecure connections.",
            "serverConnectionProblemHttps": "We couldn't connect to Acrolinx. Check that the URL is correct. If you still can't connect then contact your Acrolinx administrator. It might be that your Acrolinx Core Platform doesn’t accept secure connections.",
            "serverIsNoAcrolinxServerOrHasNoSidebar": "It looks like this isn’t an Acrolinx URL or this Acrolinx Core Platform version doesn't support the Sidebar. Ask your Acrolinx administrator to check your Acrolinx Core Platform version if you're sure that the Acrolinx URL is correct.",
            "serverIsNotSecure": "You need to connect to Acrolinx using a secure Acrolinx URL. A secure Acrolinx URL starts with \"https\"."
        },
        "placeHolder": {
            "serverAddress": "Acrolinx URL"
        },
        "title": {
            "about": "About",
            "logFile": "Log File",
            "serverAddress": "Acrolinx URL"
        },
        "tooltip": {
            "httpsRequired": "You need to connect to Acrolinx using a secure Acrolinx URL. A secure Acrolinx URL starts with \"https\".",
            "openHelp": "Open help information in a web browser window"
        }
    }
};
exports.de = {
    "serverSelector": {
        "aboutItems": {
            "browserInformation": "Informationen zum Browser",
            "logFileLocation": "Speicherort der Protokolldatei",
            "serverSelector": "Startseite",
            "startPageCorsOrigin": "CORS Origin"
        },
        "button": {
            "connect": "Verbinden",
            "details": "Details",
            "openLogFile": "Protokolldatei öffnen"
        },
        "links": {
            "about": "Über Acrolinx",
            "cantConnect": "Sie können keine Verbindung zu Acrolinx herstellen?",
            "needHelp": "Hilfe"
        },
        "message": {
            "invalidServerAddress": "Ist die Acrolinx-URL korrekt? Überprüfen Sie die eingegebene URL und versuchen Sie es erneut.",
            "loadSidebarTimeout": "Die Sidebar braucht zu lange zum Laden. Überprüfen Sie Ihre Netzwerkverbindung oder wenden Sie sich an Ihre*n Acrolinx-Admin.",
            "outdatedServer": "Ihre Acrolinx-Integration benötigt eine neuere Version der Acrolinx-Plattform. Sonst lädt die Sidebar nicht. Bitten Sie Ihre*n Admin, die Plattform zu aktualisieren.",
            "serverConnectionProblemHttp": "Wir konnten keine Verbindung zu Acrolinx herstellen. Stimmt die Acrolinx-URL? Wenn ja, wenden Sie sich an Ihre*n Admin. Möglicherweise akzeptiert Ihre Acrolinx-Plattform keine ungesicherten Verbindungen.",
            "serverConnectionProblemHttps": "Wir konnten keine Verbindung zu Acrolinx herstellen. Stimmt die Acrolinx-URL? Wenn ja, wenden Sie sich an Ihre*n Admin. Möglicherweise akzeptiert Ihre Acrolinx-Plattform keine gesicherten Verbindungen.",
            "serverIsNoAcrolinxServerOrHasNoSidebar": "Stimmt die Acrolinx-URL? Wenn ja, wird die Sidebar von der Acrolinx-Plattform evtl. nicht unterstützt? Fragen Sie Ihre*n Admin.",
            "serverIsNotSecure": "Verwenden Sie eine gesicherte Acrolinx-URL. Eine gesicherte Acrolinx-URL beginnt mit „https“."
        },
        "placeHolder": {
            "serverAddress": "Acrolinx-URL"
        },
        "title": {
            "about": "Über",
            "logFile": "Protokolldatei",
            "serverAddress": "Acrolinx-URL"
        },
        "tooltip": {
            "httpsRequired": "Verwenden Sie eine gesicherte Acrolinx-URL. Eine gesicherte Acrolinx-URL beginnt mit „https“.",
            "openHelp": "Hilfeinformationen in einem Webbrowser-Fenster öffnen"
        }
    }
};
exports.fr = {
    "serverSelector": {
        "aboutItems": {
            "browserInformation": "Informations du navigateur",
            "logFileLocation": "Emplacement des fichiers journaux",
            "serverSelector": "Page de démarrage",
            "startPageCorsOrigin": "CORS Origin"
        },
        "button": {
            "connect": "Se connecter",
            "details": "Détails",
            "openLogFile": "Ouvrir le fichier journal"
        },
        "links": {
            "about": "A propos d´Acrolinx",
            "cantConnect": "Vous ne réussissez pas à vous connecter à Acrolinx ?",
            "needHelp": "Aide"
        },
        "message": {
            "invalidServerAddress": "Il semblerait que l'URL d'Acrolinx soit incorrecte. Corriger l'adresse et essayer à nouveau.",
            "loadSidebarTimeout": "La Sidebar Acrolinx est trop lent à démarrer. Vérifier la connexion réseau ou contacter l'administrateur de Acrolinx. ",
            "outdatedServer": "Il semblerait que la version de la plateforme Acrolinx ne prenne pas en charge la Sidebar. Contactez l'administrateur Acrolinx pour une mise à jour de la plateforme.",
            "serverConnectionProblemHttp": "La connexion à Acrolinx n'a pas abouti. Est-ce que l'URL d'Acrolinx est correcte ? Si oui, contactez l'administrateur Acrolinx. Il se pourrait que votre serveur n'accepte pas de connexions non sécurisées.",
            "serverConnectionProblemHttps": "La connexion à Acrolinx n'a pas abouti. Est-ce que l'URL d'Acrolinx est correcte ? Si oui, contactez l'administrateur Acrolinx. Il se pourrait que votre serveur n'accepte pas de connexions sécurisées.",
            "serverIsNoAcrolinxServerOrHasNoSidebar": "Est-ce que l'URL d'Acrolinx est correcte ? Si oui, il se pourrait que la version de la plateforme Acrolinx ne prend pas en charge la Sidebar. Contactez l'administrateur Acrolinx pour vérifier.",
            "serverIsNotSecure": "Utilisez une URL d'Acrolinx sécurisée.  Une URL d'Acrolinx sécurisée commence par « https »."
        },
        "placeHolder": {
            "serverAddress": "URL d'Acrolinx"
        },
        "title": {
            "about": "A propos de",
            "logFile": "Fichier journal",
            "serverAddress": "URL d'Acrolinx"
        },
        "tooltip": {
            "httpsRequired": "Utilisez une URL d'Acrolinx sécurisée.  Une URL d'Acrolinx sécurisée commence par « https ».",
            "openHelp": "Ouvrir les informations d'aide dans la fenêtre d'un navigateur Web."
        }
    }
};
exports.sv = {
    "serverSelector": {
        "aboutItems": {
            "browserInformation": "Browser-information",
            "logFileLocation": "Loggfilsplats",
            "serverSelector": "Startsida",
            "startPageCorsOrigin": "CORS Origin"
        },
        "button": {
            "connect": "Anslut",
            "details": "Detaljer",
            "openLogFile": "Öppna loggfil"
        },
        "links": {
            "about": "Om Acrolinx",
            "cantConnect": "Kan du inte ansluta till Acrolinx?",
            "needHelp": "Hjälp"
        },
        "message": {
            "invalidServerAddress": "Det ser ut som om Acrolinx-URL:en är felaktig. Kontrollera adressen och försök igen.",
            "loadSidebarTimeout": "Det verkar som om det tar för lång tid att ladda Sidebar. Kontrollera din nätverksanslutning eller kontakta din Acrolinx-administratör.",
            "outdatedServer": "Det verkar som om din Acrolinx-integration behöver en nyare Acrolinx Core Platform-serverversion för att kunna ladda Sidebar. Kontakta din Acrolinx-administratör för att uppdatera Acrolinx-servern.",
            "serverConnectionProblemHttp": "Det gick inte att ansluta till Acrolinx. Kontrollera att URL:en är korrekt. Om du fortfarande inte kan ansluta till servern kontaktar du din Acrolinx-administratör. Det kan vara så att din Acrolinx Core Platform inte accepterar osäkra anslutningar.",
            "serverConnectionProblemHttps": "Det gick inte att ansluta till Acrolinx. Kontrollera att URL:en är korrekt. Om du fortfarande inte kan ansluta till servern kontaktar du din Acrolinx-administratör. Det kan vara så att din Acrolinx Core Platform inte accepterar säkra anslutningar.",
            "serverIsNoAcrolinxServerOrHasNoSidebar": "Det verkar som om detta inte är en Acrolinx-URL eller också har den här Acrolinx Core Platform-serverversionen inte stöd för Acrolinx Sidebar. Kontakta din Acrolinx-administratör och kontrollera serverversion om du inte är säker på att Acrolinx-URL:en är korrekt.",
            "serverIsNotSecure": "Du behöver ansluta till Acrolinx med en säker Acrolinx-URL. En säker Acrolinx-URL börjar med \"https\"."
        },
        "placeHolder": {
            "serverAddress": "Acrolinx-URL"
        },
        "title": {
            "about": "Om",
            "logFile": "Loggfil",
            "serverAddress": "Acrolinx-URL"
        },
        "tooltip": {
            "httpsRequired": "Du behöver ansluta till Acrolinx med en säker Acrolinx-URL. En säker Acrolinx-URL börjar med \"https\".",
            "openHelp": "Öppna hjälpinformationen i ett webbläsarfönster"
        }
    }
};
exports.ja = {
    "serverSelector": {
        "aboutItems": {
            "browserInformation": "ブラウザー情報",
            "logFileLocation": "ログファイルの保存先",
            "serverSelector": "スタートページ",
            "startPageCorsOrigin": "CORS オリジン"
        },
        "button": {
            "connect": "接続する",
            "details": "詳細",
            "openLogFile": "ログファイルを開く"
        },
        "links": {
            "about": "Acrolinxについて",
            "cantConnect": "接続に問題がありますか？",
            "needHelp": "ヘルプ"
        },
        "message": {
            "invalidServerAddress": "Acrolinx の URL に誤りがあります。URL を確認し、再試行してください。",
            "loadSidebarTimeout": "サイドバーを起動するのに時間がかかっています。ネットワークの接続を確認し、Acrolinx の管理者に連絡してください。",
            "outdatedServer": "この Acrolinx プラグインのサイドバーを起動するには、新しいコアプラットフォームバージョンが必要です。Acrolinx の管理者に、コアプラットフォームの更新を依頼してください。",
            "serverConnectionProblemHttp": "Acrolinx に接続できませんでした。URL が正しいかを確認してください。URL が正しいにも関わらず接続できない場合は、Acrolinx 管理者に連絡してください。Acrolinx コアプラットフォームが、保護されている接続だけを許可している可能性があります。",
            "serverConnectionProblemHttps": "Acrolinx に接続できませんでした。URL が正しいかを確認してください。URL が正しいにも関わらず接続できない場合は、Acrolinx 管理者に連絡してください。Acrolinx コアプラットフォームが、保護された接続を許可していない可能性があります。",
            "serverIsNoAcrolinxServerOrHasNoSidebar": "この URL は、Acrolinx の URL ではないか、このサイドバーがサポートされていないバージョンの Acrolinx コアプラットフォームです。URL が正しい場合は、Acrolinx の管理者に Acrolinx コアプラットフォームのバージョンを確認してください。",
            "serverIsNotSecure": "セキュリティーで保護されている Acrolinx URL を使用して、Acrolinx に接続してください。保護されている Acrolinx サーバーの URL は、https で始まります。"
        },
        "placeHolder": {
            "serverAddress": "Acrolinx URL"
        },
        "title": {
            "about": "バージョン",
            "logFile": "ログファイル",
            "serverAddress": "Acrolinx URL"
        },
        "tooltip": {
            "httpsRequired": "セキュリティーで保護されている Acrolinx URL を使用して、Acrolinx に接続してください。保護されている Acrolinx サーバーの URL は、https で始まります。",
            "openHelp": "ウェブブラウザーでヘルプ情報を開く"
        }
    }
};
exports.translations = {
    dev: exports.devTranslation,
    en: exports.en,
    de: exports.de,
    fr: exports.fr,
    sv: exports.sv,
    ja: exports.ja
};

},{}]},{},[16]);
