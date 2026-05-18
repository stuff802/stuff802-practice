import { defineConfig } from "vite";

export default defineConfig({
    build: {
        lib: {
            entry: "src/index.ts",
            formats: ["es"],
            fileName: "glossary-picker",
        },
        outDir: "dist",
        sourcemap: true,
        rollupOptions: {
            external: [/^@umbraco-cms\/.*/],
        },
    },
});
