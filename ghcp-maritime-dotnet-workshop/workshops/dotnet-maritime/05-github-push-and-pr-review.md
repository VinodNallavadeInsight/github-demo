# Workshop 05: Push to GitHub and Review a Pull Request with GitHub Copilot

**Duration**: 45 min | **Level**: Intermediate

## Objectives
- Create a feature branch and publish a change to GitHub
- Open a pull request (PR) with a useful description
- Request and interpret a GitHub Copilot code review
- Validate review feedback before changing or merging code

## Prerequisites
- The workshop repository is cloned locally and has a GitHub remote
- GitHub authentication is configured in VS Code or Git Credential Manager
- The user can push a branch and open a PR in the repository
- GitHub Copilot is available; Copilot code review is enabled for the repository/account if using the GitHub.com reviewer workflow
- The .NET SDK required by this repository is installed

> Use a training repository or a feature branch. Do not commit credentials, connection strings, or other secrets. Do not push directly to the default branch.

## Demo Scenario
Use a small, reviewable change in PortPulse. For example, improve a validation message or add a focused test for `ContainerNumberValidator`. Keep the change limited so participants can trace each review comment back to the diff.

## Block 1: Prepare a Branch and Change (10 min)

1. Open the repository in VS Code and confirm the current branch and working tree in Source Control.
2. Sync with the default branch, then create a feature branch such as `demo/container-validation-message`.
3. Make one small change and inspect the diff. Ask Copilot for help if useful, but review the proposed edits before accepting them.
4. Run the relevant tests and build from `demo-project/`:
   - `dotnet test`
   - `dotnet build`
5. Review the final diff for unrelated edits, generated files, secrets, and accidental configuration changes.

**Facilitator note:** Explain that Copilot-generated code is still a proposal. The author owns the change and its verification.

## Block 2: Commit and Push (8 min)

Use VS Code Source Control to stage only the intended files, enter a descriptive commit message, and commit. Publish the feature branch using **Publish Branch** or **Sync Changes**.

Equivalent Git commands, if demonstrating the terminal:

```bash
git status
git switch -c demo/container-validation-message
git add <intended-files>
git diff --cached
git commit -m "Improve container validation feedback"
git push -u origin demo/container-validation-message
```

If the branch already exists, switch to it rather than creating it again. Before pushing, confirm the staged diff and branch name. Never use `git add .` without first checking for unrelated or sensitive files.

## Block 3: Open a Pull Request (7 min)

1. On GitHub, open the repository and select **Compare & pull request** for the pushed branch, or create a PR from the **Pull requests** tab.
2. Set the default branch as the base and the feature branch as the compare branch.
3. Add a concise title and describe the change, why it is needed, and how it was tested.
4. Inspect the **Files changed** tab. Confirm the PR contains only the intended change.
5. Create the PR as a draft if it is not ready for human review; otherwise request review according to repository practice.

Suggested PR description:

```markdown
## Summary
- Improve container-number validation feedback.

## Validation
- `dotnet test`
- `dotnet build`

## Review focus
- Check validation behavior and edge cases.
```

## Block 4: Request and Assess Copilot Review (15 min)

### GitHub.com review

1. In the PR sidebar, use **Reviewers** to request a review from **Copilot**, if that option is available for this repository and account. The exact label or location can vary as GitHub updates its UI.
2. Wait for the review to complete, then read the summary and each inline comment in the context of the changed lines.
3. For each finding, classify it as **valid**, **needs investigation**, or **not applicable**. Check cited code and project conventions rather than accepting comments automatically.
4. Use the PR checks and human review as additional evidence; Copilot review does not replace either.
5. If a change is warranted, make it locally, run tests/build again, commit, and push to the same branch. The PR updates with the new commit. Reply to or resolve review threads according to repository practice.

### VS Code review alternative

If Copilot cannot be requested as a GitHub reviewer, use Copilot Chat in VS Code to inspect the PR branch diff. Provide the changed files or diff as context and ask:

> Review this pull request diff for correctness, edge cases, security, and consistency with the repository instructions. Report only actionable findings, cite the affected file and lines, explain the impact, and suggest a focused fix. Do not modify files.

Treat this as a local review aid, not as the same workflow or status as a GitHub Copilot review requested on the PR.

## Discussion
- Which comments were actionable, and what evidence confirmed that?
- Did the review catch anything tests did not? Did it suggest anything incorrect?
- What should be included in a PR description to make review more effective?
- Why should AI review be combined with tests, repository checks, and human judgment?

## Completion Checklist
- [ ] Change is on a feature branch, not the default branch
- [ ] Only intended files are committed and pushed
- [ ] PR title, summary, and validation details are clear
- [ ] Copilot review was requested, or the VS Code alternative was demonstrated
- [ ] Findings were checked and tests/build rerun for any fix
- [ ] PR is left open for normal human review and approval

## Troubleshooting and Fallback
- **No GitHub remote or push access:** Use a facilitator-owned training fork/repository, or demonstrate the steps without publishing.
- **Cannot request Copilot as reviewer:** Check repository/account availability and settings. Continue with the VS Code diff-review alternative and explain the distinction.
- **Review is delayed or unavailable:** Continue with a peer review using the same checklist; do not imply that Copilot has reviewed the PR.
- **Tests fail:** Inspect the failure before opening or updating the PR. Report known failures accurately rather than claiming validation passed.
