# Loose Doodle

Group Project for COMP397 W2022, developed by Garabatos (group 11)

## Things to Note

- Health should be 8 (from f8491641 in #26)

## Basic Game Framework

*You should always have `Persistent` scene open in the editor.* It contains the core game object called `GameManager`. Currently, the `GameManager` component, attached to the `GameManager` game object, handles all of scene loading/unloading and gameplay pausing. However, `GameManager` component is only meant to be the main interaction point between various parts of the whole gameplay. Once the `GameManager` component gets too large and complicated, we can divide tasks and responsibilities by creating new components and attaching them to the `GameManager` game object.

*When handling user input, if it should only be recognized when the game is actually playing, check `GameManager.Instance.IsGamePlaying` to see if the game is in playing mode.* You can also use `GameManager.Instance.IsGamePaused` to see if the game is paused, but keep in mind that it returns `false` in menu modes (MainMenu and GameOver scenes). `GameManager.Instance.IsGamePlaying` returns `false` when the game is paused.

*When trying to access methods of `GameManager` from the editor inspector (e.g., registering event handlers), use `GameManagerAPI` component.* Since `GameManager` game object is in a separate scene, you cannot save reference to it in the editor inspector. The `GameManagerAPI` component exposes the core functionality of the `GameManager` component. When trying to access `GameManager` component in code, you use `GameManager.Instance`.

### General Tips

1. Take advantage of the prefab system. If you see a similar structure of game objects or UI layouts across different places, making a prefab might be a good idea. You can even create a hierarchy of prefabs using prefab variants.
1. Most of the time, you can merge several game objects with 1-2 components into one game object with all of those components combined. This is the case only when all of those components are not related to the position of the game object.

## Contribution Guide

_**NEVER** push to the `main` branch. **NEVER** make commits on the `main` branch, even on your local machine._

### Implementing New Features

1. Create 1 branch for each of the feature you're going to implement. Most of the time, it corresponds to 1 task/sub-task on the Jira board.
1. When creating a branch, make sure you are on your `main` branch and have it updated to the latest version on the remote GitHub repo.
1. Name the branch to the name of the *feature* you're implementing. It's recommended to include the task/sub-task code on the Jira board. (Example: `CPG-1`, `CPG-14-repo-setup`)
1. Switch to your new branch and start working on it.
1. Make sure you create reasonably-sized commits. If you have small steps that your task can be split into, make a commit for each of those steps.
1. After your feature is implemented, push your commits to the branch (with the same name) on the remote repo.
1. Go to the GitHub repo on your web browser and click on "Pull Requests" at the top.
1. Click on the "New Pull Request" button.
1. Leave the base branch as `main`, and set the compare branch to your branch.
1. Click on "Create Pull Request" button.
1. Review the title and content of the pull request, and post your pull request.
1. While you wait for a review, move on to your next task by going back to step 1.

### After Getting Your Pull Request Reviewed

If you pull request is approved, it is going to be merged to the `main` branch almost automatically. If it is not approved, you'll be able to see comments and suggestions on your work. If the suggestions are the only changes needed, you can accept those on your web browser directly. Just make sure that you combine those into 1 commit, not 1 commit for each suggestion.

If you need to make changes using Unity and/or IDE, follow the steps below.

1. Make sure your local repository is free of any uncommitted changes. If not, either discard those (not recoverable) or create a commit for a backup.
1. Switch to the branch corresponding to the pull request.
1. Make sure you're on the latest commit that is uploaded to the remote GitHub repo.
1. Make the needed changes. Create several commits if needed.
1. After you're done, push your commits. The pull request will be automatically updated with the newly pushed commits.
1. Request another review for you pull request.

### Notes

Unity files are notorious for causing merge conflicts. Whenever merge conflicts happen, don't panic, and reach out to the lead software engineer.
