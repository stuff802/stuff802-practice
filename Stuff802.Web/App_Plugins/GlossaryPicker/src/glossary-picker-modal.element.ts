import { UmbModalBaseElement } from '@umbraco-cms/backoffice/modal';
import { html, css, customElement, state, property } from '@umbraco-cms/backoffice/external/lit';
import type { GlossaryPickerModalData, GlossaryPickerModalValue, GlossaryEntry } from './glossary-picker-modal.token.js';

@customElement('glossary-picker-modal')
export class GlossaryPickerModalElement extends UmbModalBaseElement<GlossaryPickerModalData, GlossaryPickerModalValue> {

    @state()
    private _search = '';

    private get _filtered(): GlossaryEntry[] {
        const q = this._search.toLowerCase();
        return (this.data?.entries ?? []).filter(e => e.title.toLowerCase().includes(q));
    }

    static override styles = css`
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

    override render() {
        const filtered = this._filtered;
        return html`
            <umb-body-layout headline="Insert Glossary Link">
                <uui-input
                    placeholder="Search glossary terms..."
                    .value=${this._search}
                    @input=${(e: InputEvent) => { this._search = (e.target as HTMLInputElement).value; }}
                    autofocus
                ></uui-input>

                <div class="entry-list">
                    ${filtered.length === 0
                        ? html`<div class="no-results">No matching terms found</div>`
                        : filtered.map(entry => html`
                            <div class="entry-item" @click=${() => this._pick(entry)}>
                                <span class="entry-title">${entry.title}</span>
                                <span class="entry-url">#${entry.anchor}</span>
                            </div>
                        `)
                    }
                </div>

                <uui-button slot="actions" label="Cancel" @click=${() => this._rejectModal()}></uui-button>
            </umb-body-layout>
        `;
    }

    private _pick(entry: GlossaryEntry) {
        this.value = entry;
        this._submitModal();
    }
}

export default GlossaryPickerModalElement;
