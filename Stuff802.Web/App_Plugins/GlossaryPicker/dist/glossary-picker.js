import { UmbTiptapToolbarElementApiBase as e } from "@umbraco-cms/backoffice/tiptap";
import { UMB_MODAL_MANAGER_CONTEXT as t, UmbModalBaseElement as n, UmbModalToken as r } from "@umbraco-cms/backoffice/modal";
import { css as i, customElement as a, html as o, state as s } from "@umbraco-cms/backoffice/external/lit";
//#region src/glossary-picker-modal.token.ts
var c = new r("Stuff802.Modal.GlossaryPicker", { modal: {
	type: "dialog",
	size: "small"
} });
//#endregion
//#region \0@oxc-project+runtime@0.130.0/helpers/decorate.js
function l(e, t, n, r) {
	var i = arguments.length, a = i < 3 ? t : r === null ? r = Object.getOwnPropertyDescriptor(t, n) : r, o;
	if (typeof Reflect == "object" && typeof Reflect.decorate == "function") a = Reflect.decorate(e, t, n, r);
	else for (var s = e.length - 1; s >= 0; s--) (o = e[s]) && (a = (i < 3 ? o(a) : i > 3 ? o(t, n, a) : o(t, n)) || a);
	return i > 3 && a && Object.defineProperty(t, n, a), a;
}
//#endregion
//#region src/glossary-picker-modal.element.ts
var u = class extends n {
	constructor(...e) {
		super(...e), this._search = "";
	}
	get _filtered() {
		let e = this._search.toLowerCase();
		return (this.data?.entries ?? []).filter((t) => t.title.toLowerCase().includes(e));
	}
	static {
		this.styles = i`
        uui-input {
            width: 100%;
            margin-bottom: var(--uui-size-space-4);
        }
        .entry-list {
            display: flex;
            flex-direction: column;
            gap: 2px;
            max-height: 400px;
            overflow-y: auto;
        }
        .entry-item {
            display: flex;
            align-items: center;
            padding: var(--uui-size-space-3) var(--uui-size-space-4);
            cursor: pointer;
            border-radius: var(--uui-border-radius);
            font-size: var(--uui-type-small-size);
        }
        .entry-item:hover {
            background: var(--uui-color-surface-emphasis);
        }
        .entry-title {
            font-weight: 600;
        }
        .entry-url {
            margin-left: auto;
            color: var(--uui-color-text-alt);
            font-size: 0.85em;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            max-width: 200px;
        }
        .no-results {
            padding: var(--uui-size-space-4);
            color: var(--uui-color-text-alt);
            text-align: center;
        }
    `;
	}
	render() {
		let e = this._filtered;
		return o`
            <umb-body-layout headline="Insert Glossary Link">
                <uui-input
                    placeholder="Search glossary terms..."
                    .value=${this._search}
                    @input=${(e) => {
			this._search = e.target.value;
		}}
                    autofocus
                ></uui-input>

                <div class="entry-list">
                    ${e.length === 0 ? o`<div class="no-results">No matching terms found</div>` : e.map((e) => o`
                            <div class="entry-item" @click=${() => this._pick(e)}>
                                <span class="entry-title">${e.title}</span>
                                <span class="entry-url">#${e.anchor}</span>
                            </div>
                        `)}
                </div>

                <uui-button slot="actions" label="Cancel" @click=${() => this._rejectModal()}></uui-button>
            </umb-body-layout>
        `;
	}
	_pick(e) {
		this.value = e, this._submitModal();
	}
};
l([s()], u.prototype, "_search", void 0), u = l([a("glossary-picker-modal")], u);
//#endregion
//#region src/index.ts
var d = class extends e {
	async execute(e) {
		if (!e) return;
		e.chain().focus().run();
		let n = await fetch("/umbraco/api/glossary/entries");
		if (!n.ok) return;
		let r = await n.json(), i = await (await this.getContext(t)).open(this, c, { data: { entries: r } }).onSubmit().catch(() => void 0);
		if (!i) return;
		let a = i.anchor ? `${i.url}#${i.anchor}` : i.url, { from: o, to: s } = e.state.selection;
		o === s ? e.chain().focus().insertContent(`<a href="${a}">${i.title}</a>`).run() : e.chain().focus().setLink({ href: a }).run();
	}
};
//#endregion
export { d as default };

//# sourceMappingURL=glossary-picker.js.map