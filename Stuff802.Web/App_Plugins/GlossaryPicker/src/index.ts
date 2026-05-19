import { UmbTiptapToolbarElementApiBase } from '@umbraco-cms/backoffice/tiptap';

interface GlossaryEntry {
    title: string;
    url: string;
    anchor: string;
}

export default class GlossaryPickerToolbarApi extends UmbTiptapToolbarElementApiBase {

    override async execute(editor?: any) {
        if (!editor) return;

        editor.chain().focus().run();

        const response = await fetch('/umbraco/api/glossary/entries');
        if (!response.ok) return;
        const entries: GlossaryEntry[] = await response.json();

        const entry = await this._showPicker(entries);
        if (!entry) return;

        const href = entry.anchor ? `${entry.url}#${entry.anchor}` : entry.url;
        const { from, to } = editor.state.selection;

        if (from !== to) {
            editor.chain().focus().setLink({ href }).run();
        } else {
            editor.chain().focus().insertContent(`<a href="${href}">${entry.title}</a>`).run();
        }
    }

    private _showPicker(entries: GlossaryEntry[]): Promise<GlossaryEntry | undefined> {
        return new Promise((resolve) => {
            const dialog = document.createElement('dialog');
            Object.assign(dialog.style, {
                padding: '0',
                border: 'none',
                borderRadius: '6px',
                boxShadow: '0 8px 32px rgba(0,0,0,0.3)',
                width: '480px',
                maxWidth: '90vw',
                maxHeight: '80vh',
                overflow: 'hidden',
                display: 'flex',
                flexDirection: 'column',
                fontFamily: 'sans-serif',
            });

            dialog.innerHTML = `
                <div style="padding:16px 20px 12px;border-bottom:1px solid #e0e0e0;background:#f8f8f8;">
                    <p style="margin:0 0 10px;font-size:15px;font-weight:600;color:#333;">Insert Glossary Link</p>
                    <input id="gloss-search" type="search" placeholder="Search terms…"
                        style="width:100%;padding:7px 10px;border:1px solid #ccc;border-radius:4px;font-size:14px;box-sizing:border-box;" />
                </div>
                <div id="gloss-list" style="overflow-y:auto;flex:1;max-height:380px;"></div>
                <div style="padding:10px 20px;border-top:1px solid #e0e0e0;text-align:right;background:#f8f8f8;">
                    <button id="gloss-cancel"
                        style="padding:6px 16px;border:1px solid #ccc;border-radius:4px;background:#fff;cursor:pointer;font-size:14px;">
                        Cancel
                    </button>
                </div>
            `;

            const listEl = dialog.querySelector('#gloss-list') as HTMLElement;
            const searchEl = dialog.querySelector('#gloss-search') as HTMLInputElement;

            const render = (filter: string) => {
                const filtered = entries.filter(e => e.title.toLowerCase().includes(filter.toLowerCase()));
                if (filtered.length === 0) {
                    listEl.innerHTML = '<p style="padding:16px;color:#888;text-align:center;">No matching terms</p>';
                    return;
                }
                listEl.innerHTML = filtered.map(e =>
                    `<div data-i="${entries.indexOf(e)}"
                        style="padding:9px 20px;cursor:pointer;border-bottom:1px solid #f0f0f0;font-size:14px;color:#333;">
                        ${e.title}
                    </div>`
                ).join('');
                listEl.querySelectorAll<HTMLElement>('div[data-i]').forEach(el => {
                    el.addEventListener('mouseenter', () => el.style.background = '#f0f5ff');
                    el.addEventListener('mouseleave', () => el.style.background = '');
                    el.addEventListener('click', () => {
                        resolve(entries[Number(el.dataset.i)]);
                        close();
                    });
                });
            };

            const close = () => {
                dialog.close();
                document.body.removeChild(dialog);
            };

            render('');
            searchEl.addEventListener('input', () => render(searchEl.value));
            (dialog.querySelector('#gloss-cancel') as HTMLButtonElement).addEventListener('click', () => {
                resolve(undefined);
                close();
            });
            dialog.addEventListener('close', () => {
                resolve(undefined);
                if (dialog.parentNode) document.body.removeChild(dialog);
            });

            document.body.appendChild(dialog);
            dialog.showModal();
            searchEl.focus();
        });
    }
}
